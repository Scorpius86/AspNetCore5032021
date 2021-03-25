using Net5.AspNet.MVC.Infrastructure.Data.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Repositories
{
    public class UsuarioRepository :GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(Net5FundamentalsEFDatabaseContext context) : base(context)
        {

        }
    }
}
