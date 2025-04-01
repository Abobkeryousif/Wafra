using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafra.Application.DependencyInjection
{
    public static class ApplicationRegister
    {
        public static IServiceCollection ApplicationConfig(this IServiceCollection services) 
        {
            return services;
        }
    }
}
