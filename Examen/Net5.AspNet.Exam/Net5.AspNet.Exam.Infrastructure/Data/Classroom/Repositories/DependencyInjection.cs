using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories
{
    public static class DependencyInjection
    {
        public class ClassroomRepositoriesOptions
        {
            public string ConnectionString { get; set; }
        }

        public static IServiceCollection AddClassroomRepositories(this IServiceCollection services, Action<ClassroomRepositoriesOptions> configureOptions)
        {
            var options = new ClassroomRepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ClassroomUnitOfWork>();

            services.AddDbContext<ClassroomContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });
            return services;
        }

        
    }
}
