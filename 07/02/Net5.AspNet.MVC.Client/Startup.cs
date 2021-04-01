using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net5.AspNet.MVC.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.MVC.Client.Areas.Identity.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Net5.AspNet.MVC.Client.Helper;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;
using Net5.AspNet.MVC.Infrastructure.Helper.Error;

namespace Net5.AspNet.MVC.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews(opt => {
                opt.Filters.Add(new AuthorizeFilter());
             })
                .AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();
            services.AddBlogRepositories();
            services.AddAuditRepositories();
            services.AddServices();

            services.AddScoped<LogFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandlerMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();            
            SeedData.Initialize(scope.ServiceProvider);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();
                        
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
