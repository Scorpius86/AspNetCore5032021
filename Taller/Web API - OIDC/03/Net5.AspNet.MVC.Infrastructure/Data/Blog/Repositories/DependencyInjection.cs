using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Infrastructure.Data.Base;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories
{
    public static class DependencyInjection
    {
        public class BlogRepositoriesOptions
        {
            public string ConnectionString { get; set; }
        }
        public static IServiceCollection AddBlogRepositories(this IServiceCollection services, Action<BlogRepositoriesOptions> configureOptions)
        {
            var options = new BlogRepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IComentarioRepository,ComentarioRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<BlogUnitOfWork>();

            services.AddDbContext<BlogContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });
            return services;
        }
    }
}
