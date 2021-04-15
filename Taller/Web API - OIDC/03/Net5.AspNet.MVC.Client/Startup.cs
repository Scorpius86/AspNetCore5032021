using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using IdentityModel;
using static IdentityModel.OidcConstants;
using Microsoft.AspNetCore.Http;
using Net5.AspNet.MVC.Infrastructure.Constants;
using System.Security.Claims;
using Net5.AspNet.MVC.Infrastructure.Agents;

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
             }).AddRazorRuntimeCompilation();
            
            services.AddHttpContextAccessor();            
            services.AddAuditRepositories(opt => opt.ConnectionString = Configuration.GetConnectionString("AuditContextConnection"));
            services.AddAgents(opt=>opt.UrlService = "https://localhost:44318");

            services.AddScoped<LogFilter>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.EditPost, policy => policy.RequireClaim("GrantAccess", GrantAccess.Edit));
                opt.AddPolicy(Policies.DeletePost, policy => policy.RequireClaim("GrantAccess", GrantAccess.Delete));
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(opt =>
                {
                    opt.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddOpenIdConnect(opt =>
                {
                    opt.Authority = "https://localhost:44320";
                    opt.ClientId = "Net5.AspNet.MVC.Client";
                    
                    //Store in application secrets
                    opt.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    opt.CallbackPath = "/signin-oidc";
                    opt.Scope.Add("Net5.AspNet.MVC");
                    //opt.Scope.Add("Net5.AspNet.MVC.Blog.Api");

                    opt.SaveTokens = true;

                    opt.GetClaimsFromUserInfoEndpoint = true;
                    opt.ClaimActions.MapJsonKey(ClaimTypes.Role, ClaimTypes.Role);
                    opt.ClaimActions.MapJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
                    opt.ClaimActions.MapJsonKey(SecurityClaimType.GrantAccess, SecurityClaimType.GrantAccess);

                    opt.ResponseType = ResponseTypes.Code;
                    opt.ResponseMode = ResponseModes.FormPost;

                    opt.UsePkce = true;                    
                    opt.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
                    opt.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseExceptionHandlerMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();            
            //SecuritySeedData.Initialize(scope.ServiceProvider);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
