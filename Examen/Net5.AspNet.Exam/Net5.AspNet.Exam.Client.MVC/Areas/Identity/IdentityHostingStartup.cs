using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Entities;
using Net5.AspNet.Exam.Infrastructure.Security;

[assembly: HostingStartup(typeof(Net5.AspNet.Exam.Client.MVC.Areas.Identity.IdentityHostingStartup))]
namespace Net5.AspNet.Exam.Client.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SecurityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SecurityContextConnection")));

                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<SecurityContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

                services.AddAuthorizationAndPolicies();
            });
        }
    }
}