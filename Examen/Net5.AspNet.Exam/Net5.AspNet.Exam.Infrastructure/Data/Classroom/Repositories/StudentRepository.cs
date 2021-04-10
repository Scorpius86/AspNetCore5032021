using Microsoft.EntityFrameworkCore;
using Net5.AspNet.Exam.Infrastructure.Data.Base;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private new readonly ClassroomContext _context;
        public StudentRepository(ClassroomContext context) : base(context)
        {
            _context = context;
        }

        public Student GetById(int id,string includeProperties = "")
        {
            IQueryable<Student> query = _context.Students;

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(s => s.StudentId == id);
        }
        
        public List<Student> ListStudentsWithGrades(Expression<Func<Student, bool>> filter = null)
        {
            IQueryable<Student> queryStudent = _context.Students;

            if (filter != null)
            {
                queryStudent = queryStudent.Where(filter);
            }

            var queryGradeBase = from s in queryStudent
                                 from c in _context.Courses
                                 select new { s.StudentId, c.CourseId };

            var queryGrade = from qb in queryGradeBase
                        join g in _context.Grades on new { StudenId = qb.StudentId, CourseId = qb.CourseId } equals new { StudenId = g.StudentId, CourseId = g.CourseId } into gradeJoin
                        from gradeLeft in gradeJoin.DefaultIfEmpty()
                        select new Grade
                        {
                            GradeId = gradeLeft!=null?gradeLeft.GradeId:0,
                            StudentId = qb.StudentId,
                            CourseId = qb.CourseId,
                            Value = gradeLeft != null ? gradeLeft.Value : 0,
                            CreationUserId = gradeLeft != null ? gradeLeft.CreationUserId : null,
                            CreationDate = gradeLeft != null ? gradeLeft.CreationDate : DateTime.Now,
                            UpdateUserId = gradeLeft != null ? gradeLeft.UpdateUserId : null,
                            UpdateDate = gradeLeft != null ? gradeLeft.UpdateDate : DateTime.Now
                        };

            List<Grade> grades = queryGrade.ToList();
            List<Student> students = queryStudent.ToList();
            students.ForEach(student =>
            {
                student.Grades = grades.Where(g => g.StudentId == student.StudentId).ToList();
            });

            return students;
        }
        
    }
}
