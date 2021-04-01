using Net5.AspNet.MVC.Infrastructure.Data.Base;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories
{
    public class UsuarioRepository :GenericRepository<User>, IUsuarioRepository
    {
        public UsuarioRepository(BlogContext context) : base(context)
        {

        }
    }
}
