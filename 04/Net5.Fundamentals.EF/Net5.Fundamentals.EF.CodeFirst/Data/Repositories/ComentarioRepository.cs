using Net5.Fundamentals.EF.CodeFirst.Data.Contexts;
using Net5.Fundamentals.EF.CodeFirst.Data.Entities;
using Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories
{
    public class ComentarioRepository:GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(Net5FundamentalsEFDatabaseContext context):base(context)
        {

        }
    }
}
