using Net5.AspNet.MVC.Infrastructure.Data.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base;
using Net5.AspNet.MVC.Client.Helper;
using Net5.AspNet.MVC.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<PostViewModel> ListPosts()
        {
            return Mapper.PostsToPostViewModels(_unitOfWork.Posts.GetPosts());
        }
        public PostViewModel GetPostById(int postId)
        {
            return Mapper.PostToPostViewModel(_unitOfWork.Posts.GetPostById(postId));
        }

        public void InsertComment(ComentarioViewModel comentarioViewModel)
        {
            Comentario comentario = Mapper.ComentarioViewModelToComentario(comentarioViewModel);
            comentario.Post = null;
            comentario.UsuarioIdPropietarioNavigation = null;
            comentario.UsuarioIdCreacionNavigation = null;
            comentario.UsuarioIdActualizacionNavigation = null;

            comentario.FechaCreacion = DateTime.Now;
            comentario.FechaActualizacion = DateTime.Now;
            comentario.UsuarioIdPropietario = 1;
            comentario.UsuarioIdCreacion = 1;
            comentario.UsuarioIdActualizacion = 1;

            _unitOfWork.Comentarios.Insert(comentario);
            _unitOfWork.Save();
        }

        public void InsertPost(PostViewModel postViewModel)
        {
            Post post = Mapper.PostViewModelToPost(postViewModel);

            post.Comentarios = null;
            post.UsuarioIdPropietarioNavigation = null;
            post.UsuarioIdCreacionNavigation = null;
            post.UsuarioIdActualizacionNavigation = null;

            post.FechaCreacion = DateTime.Now;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdPropietario = 1;
            post.UsuarioIdCreacion = 1;
            post.UsuarioIdActualizacion = 1;

            _unitOfWork.Posts.Insert(post);
            _unitOfWork.Save();
        }
        public void UpdatePost(PostViewModel postViewModel)
        {
            Post post = _unitOfWork.Posts.GetById(postViewModel.PostId);

            post.Titulo = postViewModel.Titulo;
            post.Resumen = postViewModel.Resumen;
            post.Contenido = postViewModel.Contenido;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdActualizacion = 1;

            _unitOfWork.Posts.Update(post);
            _unitOfWork.Save();
        }
        public bool PostExists(int postId)
        {
            Post post = _unitOfWork.Posts.GetById(postId);
            return (post != null);
        }
        public void DeletePost(int postId)
        {
            Post post = _unitOfWork.Posts.GetById(postId);
            
            _unitOfWork.Posts.Delete(post);
            _unitOfWork.Save();
        }        
    }
}
