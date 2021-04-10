using Net5.AspNet.Exam.Infrastructure.Data.Base;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories
{
    public class ClassroomUnitOfWork: UnitOfWork
    {
        public IStudentRepository Students { get; }
        public ICourseRepository Courses { get; }
        public IGradeRepository Grades { get; }
        public IUserRepository Users { get; }
        private ClassroomContext _context;

        public ClassroomUnitOfWork(
            ClassroomContext context,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IGradeRepository gradeRepository,
            IUserRepository userRepository
        ):base(context)
        {
            Students = studentRepository;
            Courses = courseRepository;
            Grades = gradeRepository;
            Users = userRepository;
        }
    }
}
