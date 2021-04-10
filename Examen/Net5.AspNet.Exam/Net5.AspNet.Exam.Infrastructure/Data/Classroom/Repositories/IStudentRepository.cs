using Net5.AspNet.Exam.Infrastructure.Data.Base;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Student GetById(int id, string includeProperties = "");
        List<Student> ListStudentsWithGrades(Expression<Func<Student, bool>> filter = null);
    }
}
