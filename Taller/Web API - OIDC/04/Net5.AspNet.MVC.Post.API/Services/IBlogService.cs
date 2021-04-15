using Net5.AspNet.MVC.Infrastructure.Dtos;
using System.Collections.Generic;

namespace Net5.AspNet.MVC.Blog.API.Services
{
    public interface IBlogService
    {
        PostDto GetPostById(int postId);
        ComentarioDto InsertComment(ComentarioDto comentarioDto);
        PostDto InsertPost(PostDto postDto);
        List<PostDto> ListPosts();
        PostDto UpdatePost(PostDto postDto);
        PostDto DeletePost(int postId);
        bool PostExists(int postId);
    }
}