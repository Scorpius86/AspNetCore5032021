using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.MVC.Infrastructure.Data.Security.Entities;

namespace Net5.AspNet.MVC.Infrastructure.Data.Security.Contexts
{
    public class SecurityContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");


            //builder.Entity<ApplicationUser>().ToTable("Usuario", "Blog").Property(p => p.PasswordHash).HasColumnName("Clave");
            //builder.Entity<ApplicationUser>().ToTable("Usuario", "Blog").Property(p => p.UserName).HasColumnName("NombreUsuario");            
        }
    }
}
