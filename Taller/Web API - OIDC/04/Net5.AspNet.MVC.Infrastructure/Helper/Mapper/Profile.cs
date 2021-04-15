using AutoMapper;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Helper.Mapper
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName} {src.SurName}"));

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UsuarioPropietario, opt => opt.MapFrom(src => src.UsuarioIdPropietarioNavigation))
                .ForMember(dest => dest.UsuarioCreacion, opt => opt.MapFrom(src => src.UsuarioIdCreacionNavigation))
                .ForMember(dest => dest.UsuarioActualizacion, opt => opt.MapFrom(src => src.UsuarioIdActualizacionNavigation));

            CreateMap<Comentario, ComentarioDto>()
                .ForMember(dest => dest.UsuarioPropietario, opt => opt.MapFrom(src => src.UsuarioIdPropietarioNavigation))
                .ForMember(dest => dest.UsuarioCreacion, opt => opt.MapFrom(src => src.UsuarioIdCreacionNavigation))
                .ForMember(dest => dest.UsuarioActualizacion, opt => opt.MapFrom(src => src.UsuarioIdActualizacionNavigation));


            CreateMap<UserDto, User>();

            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.UsuarioIdPropietarioNavigation, opt => opt.MapFrom(src => src.UsuarioPropietario))
                .ForMember(dest => dest.UsuarioIdCreacionNavigation, opt => opt.MapFrom(src => src.UsuarioCreacion))
                .ForMember(dest => dest.UsuarioIdActualizacionNavigation, opt => opt.MapFrom(src => src.UsuarioActualizacion));

            CreateMap<ComentarioDto, Comentario>()
                .ForMember(dest => dest.UsuarioIdPropietarioNavigation, opt => opt.MapFrom(src => src.UsuarioPropietario))
                .ForMember(dest => dest.UsuarioIdCreacionNavigation, opt => opt.MapFrom(src => src.UsuarioCreacion))
                .ForMember(dest => dest.UsuarioIdActualizacionNavigation, opt => opt.MapFrom(src => src.UsuarioActualizacion));
        }
    }
}


