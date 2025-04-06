using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<ISendEmail, SendEmail>();
            return services;
        }
    }
}
