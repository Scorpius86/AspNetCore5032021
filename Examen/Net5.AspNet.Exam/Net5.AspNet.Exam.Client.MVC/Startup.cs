using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net5.AspNet.Exam.Client.MVC.Services;
using Net5.AspNet.Exam.Infrastructure.Data.Audit.Repositories;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories;
using Net5.AspNet.Exam.Infrastructure.Data.Security;
using Net5.AspNet.Exam.Infrastructure.Error;
using Net5.AspNet.Exam.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC
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
            string classroomConn = Configuration.GetConnectionString("ClassroomContextConnection");
            string auditConn = Configuration.GetConnectionString("AuditContextConnection");
            
            services.AddControllersWithViews(opt => {opt.Filters.Add(new AuthorizeFilter());}).AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();
            services.AddClassroomRepositories(opt =>opt.ConnectionString = classroomConn);
            services.AddAuditRepositories(opt => opt.ConnectionString = auditConn);
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
            SecuritySeedData.Initialize(scope.ServiceProvider);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Identity",
                    areaName : "Identity",
                    pattern: "Identity/{controller=Account}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}