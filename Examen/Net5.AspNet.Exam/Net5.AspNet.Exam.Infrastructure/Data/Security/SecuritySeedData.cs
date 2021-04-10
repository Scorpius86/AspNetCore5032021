using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Entities;
using Net5.AspNet.Exam.Infrastructure.Security.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Security
{
    public static class SecuritySeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            EnsureRolesAsync(roleManager).Wait();

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            EnsureUsersAsync(userManager).Wait();
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            bool alreadyExists = await roleManager.RoleExistsAsync(Roles.Administrator);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.Administrator);
                await roleManager.CreateAsync(role);

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetUsers));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.AddUsers));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.EditUsers));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.DeleteUsers));

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetStudents));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.AddStudents));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.EditStudents));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.DeleteStudents));

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetCourses));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.AddCourses));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.EditCourses));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.DeleteCourses));

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.AddGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.EditGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.DeleteGrades));
            }

            alreadyExists = await roleManager.RoleExistsAsync(Roles.Teacher);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.Teacher);
                await roleManager.CreateAsync(role);                

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.AddGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.EditGrades));
                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.DeleteGrades));
            }

            alreadyExists = await roleManager.RoleExistsAsync(Roles.Student);
            if (!alreadyExists)
            {
                var role = new IdentityRole(Roles.Student);
                await roleManager.CreateAsync(role);

                await roleManager.AddClaimAsync(role, new Claim(SecurityClaimType.GrantAccess, GrantAccess.GetGrades));
            }
        }

        private static async Task EnsureUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.Users.Where(x => x.UserName == "admin@todo.local").SingleOrDefaultAsync();
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@todo.local",
                    Email = "admin@todo.local",
                    FirstName = "Erick",
                    LastName = "Aróstegui",
                    SurName = "Cunza"
                };
                await userManager.CreateAsync(admin, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, emailConfirmationToken);
                await userManager.AddToRoleAsync(admin, Roles.Administrator);                
            }

            var teacher = await userManager.Users.Where(x => x.UserName == "teacher@todo.local").SingleOrDefaultAsync();
            if (teacher == null)
            {
                teacher = new ApplicationUser
                {
                    UserName = "teacher@todo.local",
                    Email = "teacher@todo.local",
                    FirstName = "Teacher",
                    LastName = "User",
                    SurName = "Test"
                };
                await userManager.CreateAsync(teacher, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(teacher);
                await userManager.ConfirmEmailAsync(teacher, emailConfirmationToken);
                await userManager.AddToRoleAsync(teacher, Roles.Teacher);                
            }

            var student = await userManager.Users.Where(x => x.UserName == "student@todo.local").SingleOrDefaultAsync();
            if (student == null)
            {
                student = new ApplicationUser
                {
                    UserName = "student@todo.local",
                    Email = "student@todo.local",
                    FirstName = "Student",
                    LastName = "Test",
                    SurName = "QA"
                };
                await userManager.CreateAsync(student, "P@ssword1234");
                string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(student);
                await userManager.ConfirmEmailAsync(student, emailConfirmationToken);
                await userManager.AddToRoleAsync(student, Roles.Student);
            }
        }
    }
}
