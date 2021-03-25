using Net5.AspNet.MVC.Infrastructure.Data.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Repositories
{
    public class ComentarioRepository:GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(Net5FundamentalsEFDatabaseContext context):base(context)
        {

        }
    }
}
