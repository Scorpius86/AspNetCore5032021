using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using Net5.AspNet.MVC.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Helper
{
    public static class Mapper
    {
        public static UserViewModel UsuarioToUsuarioViewModel(User usuario)
        {
            UserViewModel usuarioViewModel = new UserViewModel
            {
                UsuarioId = usuario.Id,
                NombreUsuario = usuario.UserName,                
                Nombre = usuario.FirstName,
                ApellidoPaterno = usuario.LastName,
                ApellidoMaterno = usuario.SurName,
                NombreCompleto = $"{usuario.FirstName} {usuario.LastName} {usuario.SurName}"
            };

            return usuarioViewModel;
        }
        public static List<UserViewModel> UsuariosToUsuarioViewModels(List<User> usuarios)
        {
            List<UserViewModel> usuarioViewModels = new List<UserViewModel>();
            usuarios.ForEach(usuario => usuarioViewModels.Add(UsuarioToUsuarioViewModel(usuario)));

            return usuarioViewModels;
        }
        public static PostViewModel PostToPostViewModel(Post post)
        {
            PostViewModel postViewModel = new PostViewModel
            {
                PostId = post.PostId,
                Titulo = post.Titulo,
                Resumen = post.Resumen,
                Contenido = post.Contenido,
                FechaCreacion = post.FechaCreacion,
                FechaActualizacion = post.FechaActualizacion
            };

            if(post.Comentarios != null && post.Comentarios.Any())
            {
                postViewModel.Comentarios = ComentariosToComentarioViewModels(post.Comentarios.ToList());
            }
            if(post.UsuarioIdPropietarioNavigation != null)
            {
                postViewModel.UsuarioPropietario = UsuarioToUsuarioViewModel(post.UsuarioIdPropietarioNavigation);
            }
            if (post.UsuarioIdCreacionNavigation != null)
            {
                postViewModel.UsuarioCreacion = UsuarioToUsuarioViewModel(post.UsuarioIdCreacionNavigation);
            }
            if (post.UsuarioIdActualizacionNavigation != null)
            {
                postViewModel.UsuarioActualizacion = UsuarioToUsuarioViewModel(post.UsuarioIdActualizacionNavigation);
            }

            return postViewModel;
        }
        public static List<PostViewModel> PostsToPostViewModels(List<Post> posts)
        {
            List<PostViewModel> postViewModels = new List<PostViewModel>();
            posts.ForEach(post => postViewModels.Add(PostToPostViewModel(post)));

            return postViewModels;
        }
        public static ComentarioViewModel ComentarioToComentarioViewModel(Comentario comentario)
        {
            ComentarioViewModel comentarioViewModel = new ComentarioViewModel
            {
                ComentarioId = comentario.ComentarioId,
                PostId = comentario.PostId,
                Contenido = comentario.Contenido,
                FechaCreacion = comentario.FechaCreacion,
                FechaActualizacion = comentario.FechaActualizacion,
                UsuarioIdPropietario = comentario.UsuarioIdPropietario,
                UsuarioIdCreacion = comentario.UsuarioIdCreacion,
                UsuarioIdActualizacion = comentario.UsuarioIdActualizacion
            };

            if (comentario.Post != null )
            {
                comentarioViewModel.Post = PostToPostViewModel(comentario.Post);
            }
            if (comentario.UsuarioIdPropietarioNavigation != null )
            {
                comentarioViewModel.UsuarioPropietario = UsuarioToUsuarioViewModel(comentario.UsuarioIdPropietarioNavigation);
            }
            if (comentario.UsuarioIdCreacionNavigation != null)
            {
                comentarioViewModel.UsuarioCreacion = UsuarioToUsuarioViewModel(comentario.UsuarioIdCreacionNavigation);
            }
            if (comentario.UsuarioIdActualizacionNavigation != null)
            {
                comentarioViewModel.UsuarioActualizacion = UsuarioToUsuarioViewModel(comentario.UsuarioIdActualizacionNavigation);
            }

            return comentarioViewModel;
        }
        public static List<ComentarioViewModel> ComentariosToComentarioViewModels(List<Comentario> comentarios)
        {
            List<ComentarioViewModel> comentarioViewModels = new List<ComentarioViewModel>();
            comentarios.ForEach(comentario => comentarioViewModels.Add(ComentarioToComentarioViewModel(comentario)));

            return comentarioViewModels;
        }

        public static User UsuarioViewModelToUsuario(UserViewModel usuarioViewModel)
        {
            User usuario = new User
            {
                Id = usuarioViewModel.UsuarioId,
                UserName = usuarioViewModel.NombreUsuario,                
                FirstName = usuarioViewModel.Nombre,
                LastName = usuarioViewModel.ApellidoPaterno,
                SurName = usuarioViewModel.ApellidoMaterno
            };

            return usuario;
        }
        public static List<User> usuarioViewModelsToUsuario(List<UserViewModel> usuarioViewModels)
        {
            List<User> usuarios = new List<User>();
            usuarioViewModels.ForEach(usuarioViewModel => usuarios.Add(UsuarioViewModelToUsuario(usuarioViewModel)));

            return usuarios;
        }
        public static Post PostViewModelToPost(PostViewModel postViewModel)
        {
            Post post = new Post
            {
                PostId = postViewModel.PostId,
                Titulo = postViewModel.Titulo,
                Resumen = postViewModel.Resumen,
                Contenido = postViewModel.Contenido,
                FechaCreacion = postViewModel.FechaCreacion,
                FechaActualizacion = postViewModel.FechaActualizacion
            };

            if (postViewModel.Comentarios != null && postViewModel.Comentarios.Any())
            {
                post.Comentarios = ComentarioViewModelsToComentarios(postViewModel.Comentarios.ToList()).ToList();
            }
            if (postViewModel.UsuarioPropietario != null)
            {                
                post.UsuarioIdPropietarioNavigation = UsuarioViewModelToUsuario(postViewModel.UsuarioPropietario);
            }
            if (postViewModel.UsuarioCreacion != null)
            {
                post.UsuarioIdCreacionNavigation = UsuarioViewModelToUsuario(postViewModel.UsuarioCreacion);
            }
            if (postViewModel.UsuarioActualizacion != null)
            {
                post.UsuarioIdActualizacionNavigation = UsuarioViewModelToUsuario(postViewModel.UsuarioActualizacion);
            }

            return post;
        }
        public static List<Post> PostsViewModelsToPosts(List<PostViewModel> postViewModels)
        {
            List<Post> posts = new List<Post>();
            postViewModels.ForEach(postViewModel => posts.Add(PostViewModelToPost(postViewModel)));

            return posts;
        }
        public static Comentario ComentarioViewModelToComentario(ComentarioViewModel comentarioViewModel)
        {
            Comentario comentario = new Comentario
            {
                ComentarioId = comentarioViewModel.ComentarioId,
                PostId = comentarioViewModel.PostId,
                Contenido = comentarioViewModel.Contenido,
                FechaCreacion = comentarioViewModel.FechaCreacion,
                FechaActualizacion = comentarioViewModel.FechaActualizacion,
                UsuarioIdPropietario = comentarioViewModel.UsuarioIdPropietario,
                UsuarioIdCreacion = comentarioViewModel.UsuarioIdCreacion,
                UsuarioIdActualizacion = comentarioViewModel.UsuarioIdActualizacion
            };

            if (comentarioViewModel.Post != null)
            {
                comentario.Post = PostViewModelToPost(comentarioViewModel.Post);
            }
            if (comentarioViewModel.UsuarioPropietario != null)
            {
                comentario.UsuarioIdPropietarioNavigation = UsuarioViewModelToUsuario(comentarioViewModel.UsuarioPropietario);
            }
            if (comentarioViewModel.UsuarioCreacion != null)
            {
                comentario.UsuarioIdCreacionNavigation = UsuarioViewModelToUsuario(comentarioViewModel.UsuarioCreacion);
            }
            if (comentarioViewModel.UsuarioActualizacion != null)
            {
                comentario.UsuarioIdActualizacionNavigation = UsuarioViewModelToUsuario(comentarioViewModel.UsuarioActualizacion);
            }

            return comentario;
        }
        public static List<Comentario> ComentarioViewModelsToComentarios(List<ComentarioViewModel> comentarioViewModels)
        {
            List<Comentario> comentarios = new List<Comentario>();
            comentarioViewModels.ForEach(comentarioViewModel=> comentarios.Add(ComentarioViewModelToComentario(comentarioViewModel)));

            return comentarios;
        }
    }
}
