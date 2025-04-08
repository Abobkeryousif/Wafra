using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Application.Contracts.Services;
using Wafra.Infrastructure.Data;
using Wafra.Infrastructure.Repository;

namespace Wafra.Infrastructure.DependencyInjection
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection InfrastructureConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPharamcyRepository , PharmacyRepository>();
            services.AddScoped<IMedicineRepository, MedicinRepository>();
            services.AddScoped<IUserRepository , UserRepository>();
            services.AddTransient<ISendEmail, SendEmail>();
            services.AddTransient<IOtpRepository , OtpRepostiory>();

            //token Method
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(j=> j.TokenValidationParameters = new TokenValidationParameters
                { 
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,    
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issure"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                });

            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }
    }
}
