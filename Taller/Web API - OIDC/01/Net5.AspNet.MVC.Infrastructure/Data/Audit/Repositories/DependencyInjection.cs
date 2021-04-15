using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories
{
    public static class DependencyInjection
    {
        public class AuditRepositoriesOptions
        {
            public string ConnectionString { get; set; }
        }
        public static IServiceCollection AddAuditRepositories(this IServiceCollection services, Action<AuditRepositoriesOptions> configureOptions)
        {
            var options = new AuditRepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IAuditLogRepository,AuditLogRepository>();            
            services.AddScoped<AuditContext>();
            services.AddScoped<AuditUnitOfWork>();

            services.AddDbContext<AuditContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });
            return services;
        }
    }
}
