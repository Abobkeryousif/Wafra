using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Custom_Observability_Signals.CustomTraces;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Application.Observability.Helper;
using Wafra.Core.Common;

namespace Wafra.Application.Feature.Commands.Authentication
{
    public record LoginCommand(LoginUserDto users) : IRequest<HttpResult<AuthResult>>;
    public class LoginCommandHandler : IRequestHandler<LoginCommand, HttpResult<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository,
            IRefreshTokenRepository refreshTokenRepository, ILogger<LoginCommandHandler> logger)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public async Task<HttpResult<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            const string module = "user";
            const string operation = "login";

            using var activity =
                UserActivitySource.Source.StartActivity("user.login");

            try
            {
                var loginUser = await _userRepository.FirstOrDefaultAsync(
                    u => u.Email == request.users.Email);

                if (loginUser == null)
                {
                    _logger.LogWarning("Invalid email or password");

                    activity?.SetTag("wafra.user.login.outcome", "failure");
                    BusinessMetricsHelper.RecordOperation(
                        module, operation, "failure");

                    return new HttpResult<AuthResult>(
                        HttpStatusCode.NotFound,
                        "Email or Password Is Invalid");
                }

                var pass = BCrypt.Net.BCrypt.Verify(
                    request.users.Password,
                    loginUser.Password);

                if (!pass)
                {
                    _logger.LogWarning("Invalid email or password");

                    activity?.SetTag("wafra.user.login.outcome", "failure");
                    BusinessMetricsHelper.RecordOperation(
                        module, operation, "failure");

                    return new HttpResult<AuthResult>(
                        HttpStatusCode.NotFound,
                        "Email or Password Is Invalid");
                }

                var accessToken = _tokenRepository.CreateToken(loginUser);

                var refreshTokenUser =
                    await _refreshTokenRepository.FirstOrDefaultAsync(
                        t => t.userId == loginUser.Id,
                        t => t.OrderByDescending(m => m.CreatedOn));

                if (refreshTokenUser is not null && refreshTokenUser.IsActive)
                {
                    _logger.LogInformation(
                        "Successfully authenticated user {UserId}",
                        loginUser.Id);

                    activity?.SetTag("wafra.user.login.outcome", "success");
                    BusinessMetricsHelper.RecordOperation(
                        module, operation, "success");

                    return new HttpResult<AuthResult>(
                        HttpStatusCode.OK,
                        "Login Success",
                        new AuthResult
                        {
                            AccessToken = accessToken,
                            RefreshToken = refreshTokenUser.Token,
                            ExpierdOn = refreshTokenUser.ExpierOn
                        });
                }

                var refreshToken = _tokenRepository.GenerateRefreshToken();
                refreshToken.userId = loginUser.Id;

                await _refreshTokenRepository.CreateAsync(refreshToken);

                _logger.LogInformation(
                    "Successfully authenticated user {UserId}",
                    loginUser.Id);

                activity?.SetTag("wafra.user.login.outcome", "success");
                BusinessMetricsHelper.RecordOperation(
                    module, operation, "success");

                return new HttpResult<AuthResult>(
                    HttpStatusCode.OK,
                    "Complete Login Operation",
                    new AuthResult
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken.Token,
                        ExpierdOn = refreshToken.ExpierOn
                    });
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Request cancllation is not necessarili a system error.
                throw;
            }
            catch (Exception ex)
            {
                activity?.SetTag("wafra.user.login.outcome", "error");

                activity?.SetStatus(
                    ActivityStatusCode.Error,
                    ex.GetType().Name);

                activity?.AddEvent(new ActivityEvent(
                    "exception",
                    tags: new ActivityTagsCollection
                    {
                { "exception.type", ex.GetType().FullName },
                { "exception.message", ex.Message }
                    }));

                _logger.LogError(
                    ex,
                    "Unexpected error during user login");

                BusinessMetricsHelper.RecordOperation(
                    module, operation, "error");

                throw;
            }
        }
    }
}
