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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private new readonly ClassroomContext _context;
        public UserRepository(ClassroomContext context) : base(context)
        {
            _context = context;
        }

        public List<User> ListFreeUser()
        {
            var query = from u in _context.Users
                        join s in _context.Students on u.Id equals s.UserId into studentJoins
                        from studentLeft in studentJoins.DefaultIfEmpty()
                        where studentLeft.UserId == null
                        select u;

            return query.ToList();
        }
    }
}
