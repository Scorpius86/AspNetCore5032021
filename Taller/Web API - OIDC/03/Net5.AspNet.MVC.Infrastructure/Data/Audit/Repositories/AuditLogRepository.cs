using Net5.AspNet.MVC.Infrastructure.Data.Audit.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories
{
    public class AuditLogRepository :GenericRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(AuditContext context) : base(context)
        {

        }
    }
}
