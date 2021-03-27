using Net5.AspNet.MVC.Client.Helper;
using Net5.AspNet.MVC.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.MVC.Client.Areas.Identity.Data;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Contexts;
using Net5.AspNet.MVC.Infrastructure.Data.Base;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;

namespace Net5.AspNet.MVC.Client.Services
{    
    public class BlogService : IBlogService
    {
        private readonly BlogUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public BlogService(
            BlogUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<PostViewModel> ListPosts()
        {
            _unitOfWork.Posts.GetAll();
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

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            comentario.FechaCreacion = DateTime.Now;
            comentario.FechaActualizacion = DateTime.Now;
            comentario.UsuarioIdPropietario = userId;
            comentario.UsuarioIdCreacion = userId;
            comentario.UsuarioIdActualizacion = userId;
                        
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

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            post.FechaCreacion = DateTime.Now;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdPropietario = userId;
            post.UsuarioIdCreacion = userId;
            post.UsuarioIdActualizacion = userId;

            _unitOfWork.Posts.Insert(post);
            _unitOfWork.Save();
        }
        public void UpdatePost(PostViewModel postViewModel)
        {
            Post post = _unitOfWork.Posts.GetById(postViewModel.PostId);

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            post.Titulo = postViewModel.Titulo;
            post.Resumen = postViewModel.Resumen;
            post.Contenido = postViewModel.Contenido;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdActualizacion = userId;

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
