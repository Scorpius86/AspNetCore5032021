using Net5.AspNet.MVC.Infrastructure.Data.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        List<Post> GetPostByUsuarioIdPropetario(string usuarioId);
        List<Post> GetPosts();
        Post GetPostById(int postId);
    }
}
