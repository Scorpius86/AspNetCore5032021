using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Repositories;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using AutoMapper;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using System.Security.Claims;

namespace Net5.AspNet.MVC.Blog.API.Services
{    
    public class BlogService : IBlogService
    {
        private readonly BlogUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;        
        private readonly IMapper _mapper;
        public BlogService(
            BlogUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,            
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;            
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public List<PostDto> ListPosts()
        {
            _unitOfWork.Posts.GetAll();
            return _mapper.Map<List<PostDto>>(_unitOfWork.Posts.GetPosts());
        }
        public PostDto GetPostById(int postId)
        {
            return _mapper.Map<PostDto>(_unitOfWork.Posts.GetPostById(postId));
        }

        public ComentarioDto InsertComment(ComentarioDto comentarioDto)
        {
            Comentario comentario = _mapper.Map<Comentario>(comentarioDto);
            comentario.Post = null;
            comentario.UsuarioIdPropietarioNavigation = null;
            comentario.UsuarioIdCreacionNavigation = null;
            comentario.UsuarioIdActualizacionNavigation = null;

            string userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            comentario.FechaCreacion = DateTime.Now;
            comentario.FechaActualizacion = DateTime.Now;
            comentario.UsuarioIdPropietario = userId;
            comentario.UsuarioIdCreacion = userId;
            comentario.UsuarioIdActualizacion = userId;
                        
            _unitOfWork.Comentarios.Insert(comentario);
            _unitOfWork.Save();

            return _mapper.Map<ComentarioDto>(comentario);
        }

        public PostDto InsertPost(PostDto postDto)
        {
            Post post = _mapper.Map<Post>(postDto);

            post.Comentarios = null;
            post.UsuarioIdPropietarioNavigation = null;
            post.UsuarioIdCreacionNavigation = null;
            post.UsuarioIdActualizacionNavigation = null;

            string userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            post.FechaCreacion = DateTime.Now;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdPropietario = userId;
            post.UsuarioIdCreacion = userId;
            post.UsuarioIdActualizacion = userId;

            _unitOfWork.Posts.Insert(post);
            _unitOfWork.Save();

            return _mapper.Map<PostDto>(post);
        }
        public PostDto UpdatePost(PostDto postDto)
        {
            Post post = _unitOfWork.Posts.GetById(postDto.PostId);

            string userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            post.Titulo = postDto.Titulo;
            post.Resumen = postDto.Resumen;
            post.Contenido = postDto.Contenido;
            post.FechaActualizacion = DateTime.Now;
            post.UsuarioIdActualizacion = userId;

            _unitOfWork.Posts.Update(post);
            _unitOfWork.Save();

            return _mapper.Map<PostDto>(post);
        }
        public bool PostExists(int postId)
        {
            Post post = _unitOfWork.Posts.GetById(postId);
            return (post != null);
        }
        public PostDto DeletePost(int postId)
        {
            Post post = _unitOfWork.Posts.GetById(postId);
            
            _unitOfWork.Posts.Delete(post);
            _unitOfWork.Save();

            return _mapper.Map<PostDto>(post);
        }        
    }
}
