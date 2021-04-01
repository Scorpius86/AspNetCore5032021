using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Client.Areas.Identity.Data;
using Net5.AspNet.MVC.Client.Data;
using Net5.AspNet.MVC.Client.Helper;

[assembly: HostingStartup(typeof(Net5.AspNet.MVC.Client.Areas.Identity.IdentityHostingStartup))]
namespace Net5.AspNet.MVC.Client.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SecurityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SecurityContextConnection")));
                /*
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SecurityContext>();
                */

                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<SecurityContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

                services.AddAuthorization(opt =>
                {
                    opt.AddPolicy(Policies.EditPost, policy => policy.RequireClaim("GrantAccess", GrantAccess.Edit));
                    opt.AddPolicy(Policies.DeletePost, policy => policy.RequireClaim("GrantAccess", GrantAccess.Delete));
                });
            });

        }
    }
}