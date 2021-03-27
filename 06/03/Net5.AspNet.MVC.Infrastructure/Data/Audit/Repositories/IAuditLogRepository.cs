using Net5.AspNet.MVC.Infrastructure.Data.Audit.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories
{
    public interface IAuditLogRepository:IGenericRepository<AuditLog>
    {
    }
}
