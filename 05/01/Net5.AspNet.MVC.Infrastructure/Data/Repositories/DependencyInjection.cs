using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Infrastructure.Data.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            string conn = "Data Source=.;Initial Catalog=Net5.Fundamentals.EF.Database;User ID=sa;Password=Password1234";

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IComentarioRepository,ComentarioRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<Net5FundamentalsEFDatabaseContext>(opt =>
            {
                opt.UseSqlServer(conn);
            });
            return services;
        }
    }
}
