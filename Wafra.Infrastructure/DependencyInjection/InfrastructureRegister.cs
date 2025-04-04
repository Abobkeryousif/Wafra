using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wafra.Application.Contracts.Interfaces;
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
            return services;
        }
    }
}
