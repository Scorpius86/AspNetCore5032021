using AutoMapper;
using Net5.AspNet.MVC.Client.Models;
using Net5.AspNet.MVC.Infrastructure.Data.Blog.Entities;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Helper.Mapper
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<UserDto, UserViewModel>();
            CreateMap<PostDto, PostViewModel>();                
            CreateMap<ComentarioDto, ComentarioViewModel>();

            CreateMap<UserViewModel, UserDto>();
            CreateMap<PostViewModel, PostDto>();
            CreateMap<ComentarioViewModel, ComentarioDto>();
        }
    }
}


