using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Mapping;

namespace Wafra.Application.DependencyInjection
{
    public static class ApplicationRegister
    {
        public static IServiceCollection ApplicationConfig(this IServiceCollection services) 

        {
            
            services.AddMediatR(s=> s.RegisterServicesFromAssemblies(typeof(ApplicationRegister).Assembly));
            services.AddAutoMapper(typeof(AutoMapping));
            return services;
        }
    }
}
