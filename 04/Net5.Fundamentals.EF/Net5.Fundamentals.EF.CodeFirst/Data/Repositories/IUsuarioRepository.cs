using Net5.Fundamentals.EF.CodeFirst.Data.Entities;
using Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories
{
    public interface IUsuarioRepository:IGenericRepository<Usuario>
    {
    }
}
