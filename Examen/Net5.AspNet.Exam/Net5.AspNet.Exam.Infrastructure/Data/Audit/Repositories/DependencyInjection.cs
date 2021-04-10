using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.Exam.Infrastructure.Data.Audit.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Audit.Repositories
{
    public class AuditRepositoriesOptions
    {
        public string ConnectionString { get; set; }
    }
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuditRepositories(this IServiceCollection services, Action<AuditRepositoriesOptions> configureOptions)
        {
            var options = new AuditRepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
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
