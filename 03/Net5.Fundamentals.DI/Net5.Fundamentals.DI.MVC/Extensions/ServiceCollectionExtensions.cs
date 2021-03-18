using Net5.Fundamentals.DI.MVC.ApplicationService.Core;
using Net5.Fundamentals.DI.MVC.ApplicationService.Scoped;
using Net5.Fundamentals.DI.MVC.ApplicationService.Singleton;
using Net5.Fundamentals.DI.MVC.ApplicationService.Transient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDI(this IServiceCollection services)
        {
            services.AddTransient<IGuidTransientApplicationService, GuidTransientApplicationService>();
            services.AddScoped<IGuidScopedApplicationService, GuidScopedApplicationService>();
            services.AddSingleton<IGuidSingletonApplicationService, GuidSingletonApplicationService>();

            services.AddTransient<ICoreApplicationService, CoreApplicationService>();

            return services;
        }
    }
}
