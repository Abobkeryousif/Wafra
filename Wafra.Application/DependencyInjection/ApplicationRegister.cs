using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Application.Contracts.Services;
using Wafra.Application.Mapping;
using Wafra.Core.Common;

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
