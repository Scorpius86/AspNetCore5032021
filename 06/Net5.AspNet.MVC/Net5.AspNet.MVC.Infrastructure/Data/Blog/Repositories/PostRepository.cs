using Net5.AspNet.MVC.Infrastructure.Data.Base;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories
{
    public class PostRepository:GenericRepository<Post>, IPostRepository
    {
        private new readonly BlogContext _context;
        public PostRepository(BlogContext context):base(context)
        {
            _context = context;
        }
        public List<Post> GetPostByUsuarioIdPropetario(string usuarioId)
        {
            return _context.Posts.Where(p => p.UsuarioIdPropietario == usuarioId).ToList();
        }
        public List<Post> GetPosts()
        {
            var queryPosts = from p in _context.Posts
                        join up in _context.Users on p.UsuarioIdPropietario equals up.Id
                        select new Post
                        {
                            PostId = p.PostId,
                            Titulo = p.Titulo,
                            Resumen = p.Resumen,
                            Contenido = p.Contenido,
                            FechaActualizacion = p.FechaActualizacion,
                            FechaCreacion = p.FechaCreacion,
                            UsuarioIdPropietario = p.UsuarioIdPropietario,
                            UsuarioIdPropietarioNavigation = up
                        };

            var queryCometarios = from c in _context.Comentarios
                                  join up in _context.Users on c.UsuarioIdPropietario equals up.Id
                                  select new Comentario
                                  {
                                      ComentarioId = c.ComentarioId,
                                      PostId = c.PostId,
                                      Contenido = c.Contenido,
                                      FechaActualizacion = c.FechaActualizacion,
                                      UsuarioIdPropietarioNavigation = up
                                  };

            List<Post> posts = queryPosts.ToList();
            List<Comentario> comentarios = queryCometarios.ToList();

            posts.ForEach(post =>
            {
                post.Comentarios = comentarios.Where(c => c.PostId == post.PostId).ToList();
            });

            return posts;
        }
        public Post GetPostById(int postId)
        {
            var queryPost = from p in _context.Posts
                             join up in _context.Users on p.UsuarioIdPropietario equals up.Id
                             where p.PostId == postId
                             select new Post
                             {
                                 PostId = p.PostId,
                                 Titulo = p.Titulo,
                                 Resumen = p.Resumen,
                                 Contenido = p.Contenido,
                                 FechaActualizacion = p.FechaActualizacion,
                                 FechaCreacion = p.FechaCreacion,
                                 UsuarioIdPropietario = p.UsuarioIdPropietario,
                                 UsuarioIdPropietarioNavigation = up
                             };

            var queryConetarios = from c in _context.Comentarios
                                  join up in _context.Users on c.UsuarioIdPropietario equals up.Id
                                  where c.PostId == postId
                                  select new Comentario
                                  {
                                      ComentarioId = c.ComentarioId,
                                      PostId = c.PostId,
                                      Contenido = c.Contenido,
                                      FechaActualizacion = c.FechaActualizacion,
                                      UsuarioIdPropietarioNavigation = up
                                  };

            Post post = queryPost.FirstOrDefault();            
            post.Comentarios = queryConetarios.ToList();
            
            return post;
        }
    }
}
