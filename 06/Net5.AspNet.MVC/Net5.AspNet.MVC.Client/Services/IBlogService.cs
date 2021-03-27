using Net5.AspNet.MVC.Client.Models;
using System.Collections.Generic;

namespace Net5.AspNet.MVC.Client.Services
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