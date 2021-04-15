using Net5.AspNet.MVC.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Agents.Posts
{
    public interface IPostAgent
    {
        Task<PostDto> DeletePostAsync(int postId);
        Task<PostDto> GetPostByIdAsync(int postId);
        Task<PostDto> InsertPostAsync(PostDto postDto);
        Task<List<PostDto>> ListPostsAsync();
        Task<PostDto> UpdatePostAsync(int postId, PostDto postDto);
        Task<bool> PostExistsAsync(int postId);
    }
}