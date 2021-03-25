using Net5.Fundamentals.EF.MVC.Models;
using System.Collections.Generic;

namespace Net5.Fundamentals.EF.MVC.Services
{
    public interface IBlogService
    {
        PostViewModel GetPostById(int postId);
        void InsertComment(ComentarioViewModel comentarioViewModel);
        void InsertPost(PostViewModel postViewModel);
        List<PostViewModel> ListPosts();
        void UpdatePost(PostViewModel postViewModel);
        void DeletePost(int postId);
        bool PostExists(int postId);
    }
}