using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.Exam.Infrastructure.Security.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Security
{
    public static class PolicyExtensions
    {
        public static IServiceCollection AddAuthorizationAndPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationCore(opt =>
            {
                opt.AddPolicy(Policies.GetUsers, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.GetUsers));
                opt.AddPolicy(Policies.AddUsers, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.AddUsers));
                opt.AddPolicy(Policies.EditUsers, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.EditUsers));
                opt.AddPolicy(Policies.DeleteUsers, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.DeleteUsers));

                opt.AddPolicy(Policies.GetStudents, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.GetStudents));
                opt.AddPolicy(Policies.AddStudents, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.AddStudents));
                opt.AddPolicy(Policies.EditStudents, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.EditStudents));
                opt.AddPolicy(Policies.DeleteStudents, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.DeleteStudents));

                opt.AddPolicy(Policies.GetCourses, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.GetCourses));
                opt.AddPolicy(Policies.AddCourses, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.AddCourses));
                opt.AddPolicy(Policies.EditCourses, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.EditCourses));
                opt.AddPolicy(Policies.DeleteCourses, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.DeleteCourses));

                opt.AddPolicy(Policies.GetGrades, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.GetGrades));
                opt.AddPolicy(Policies.AddGrades, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.AddGrades));
                opt.AddPolicy(Policies.EditGrades, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.EditGrades));
                opt.AddPolicy(Policies.DeleteGrades, policy => policy.RequireClaim(SecurityClaimType.GrantAccess, GrantAccess.DeleteGrades));
            });

            return services;
        }
    }
}
