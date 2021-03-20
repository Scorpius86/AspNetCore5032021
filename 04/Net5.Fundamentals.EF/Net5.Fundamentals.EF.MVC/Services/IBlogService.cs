using Net5.Fundamentals.EF.MVC.Models;
using System.Collections.Generic;

namespace Net5.Fundamentals.EF.MVC.Services
{
    public interface IBlogService
    {
        List<PostViewModel> ListPosts();
        PostViewModel GetPostById(int postId);
    }
}