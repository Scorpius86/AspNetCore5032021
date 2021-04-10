using Net5.AspNet.Exam.Infrastructure.Data.Audit.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Audit.Entities;
using Net5.AspNet.Exam.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Audit.Repositories
{
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(AuditContext context) : base(context)
        {

        }
    }
}
