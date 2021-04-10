using Net5.AspNet.Exam.Infrastructure.Data.Base;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        List<Course> ListFreeCourse(int studentId);
    }
}
