using Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base;
using Net5.Fundamentals.EF.MVC.Helper;
using Net5.Fundamentals.EF.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.EF.MVC.Services
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
            return Mapper.PostsToPostViewModels(_unitOfWork.Posts.GetAll());
        }
    }
}
