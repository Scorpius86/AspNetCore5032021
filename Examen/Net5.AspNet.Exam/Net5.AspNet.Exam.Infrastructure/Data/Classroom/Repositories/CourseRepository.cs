using Microsoft.EntityFrameworkCore;
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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private new readonly ClassroomContext _context;
        public CourseRepository(ClassroomContext context) : base(context)
        {
            _context = context;
        }
                
        public List<Course> ListFreeCourse(int studentId)
        {
            var query = from c in _context.Courses
                        join g in _context.Grades.Where(g=>g.StudentId == studentId) on c.CourseId equals g.CourseId into gradeJoin
                        from gradeLeft in gradeJoin.DefaultIfEmpty()
                        join s in _context.Students.Where(s=>s.StudentId == studentId) on gradeLeft.StudentId equals s.StudentId into studentJoin
                        from studentLeft in studentJoin.DefaultIfEmpty()
                        where studentLeft == null
                        select c;

            return query.ToList();
        }
    }
}
