using Net5.AspNet.Exam.Infrastructure.Data.Audit.Contexts;
using Net5.AspNet.Exam.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Data.Audit.Repositories
{
    public class AuditUnitOfWork: UnitOfWork
    {
        public IAuditLogRepository AuditLogRepository { get; }
        private AuditContext _context;
        public AuditUnitOfWork(
            AuditContext context,
            IAuditLogRepository auditLogRepository
        ) : base(context)
        {            
            AuditLogRepository = auditLogRepository;
        }
    }
}
