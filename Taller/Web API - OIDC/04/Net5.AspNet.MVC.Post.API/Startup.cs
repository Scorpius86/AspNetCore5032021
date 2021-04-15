using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories;
using Net5.AspNet.MVC.Infrastructure.Helper.Mapper;
using Net5.AspNet.MVC.Blog.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc.Authorization;
using IdentityModel;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;

namespace Net5.AspNet.MVC.Blog.API
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
            services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddAutoMapper(typeof(Profile).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net5.AspNet.MVC.Post.API", Version = "v1" });
            });

            services.AddHttpContextAccessor();
            services.AddAuditRepositories(opt => opt.ConnectionString = Configuration.GetConnectionString("AuditContextConnection"));
            services.AddBlogRepositories(opt => opt.ConnectionString = Configuration.GetConnectionString("BlogContextConnection"));
            services.AddServices();

            services.AddScoped<LogFilter>();

            services.AddAuthentication(
                IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "https://localhost:44320";                    
                    opt.ApiName = "Net5.AspNet.MVC.Blog.API";
                    opt.ApiSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    opt.RoleClaimType = JwtClaimTypes.Role;
                    opt.NameClaimType = JwtClaimTypes.Name;                   
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net5.AspNet.MVC.Post.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
