using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.Exam.Client.MVC.Models;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Entities;

namespace Net5.AspNet.Exam.Client.MVC.Helper.Mapper
{
    public class Profile: AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();

            CreateMap<IdentityRole, RoleViewModel>();
            
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.FullName,opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName} {src.SurName}"));
            CreateMap<StudentViewModel, Student>();

            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();

            CreateMap<Grade, GradeViewModel>();
            CreateMap<GradeViewModel, Grade>();
        }        
    }
}
