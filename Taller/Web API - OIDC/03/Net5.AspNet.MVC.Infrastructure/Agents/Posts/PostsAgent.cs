using Net5.AspNet.MVC.Infrastructure.Dtos;
using Net5.AspNet.MVC.Infrastructure.Helper.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Agents.Posts
{
    public class PostsAgent : IPostAgent
    {
        private readonly HttpClient _client;
        private readonly string _basePath = "/api/Posts";

        public PostsAgent(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<PostDto>> ListPostsAsync()
        {
            var response = await _client.GetAsync(_basePath);
            return await response.ReadContentAs<List<PostDto>>(); ;
        }
        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            string uri = $"{_basePath}/{postId}";
            var response = await _client.GetAsync(uri);
            return await response.ReadContentAs<PostDto>(); ;
        }
        public async Task<PostDto> InsertPostAsync(PostDto postDto)
        {
            string uri = $"{_basePath}";
            var response = await _client.PostAsJsonAsync(uri, postDto);
            return await response.ReadContentAs<PostDto>();
        }
        public async Task<PostDto> UpdatePostAsync(int postId, PostDto postDto)
        {
            string uri = $"{_basePath}/{postId}";
            var response = await _client.PutAsJsonAsync(uri, postDto);
            return await response.ReadContentAs<PostDto>();
        }
        public async Task<PostDto> DeletePostAsync(int postId)
        {
            string uri = $"{_basePath}/{postId}";
            var response = await _client.DeleteAsync(uri);
            return await response.ReadContentAs<PostDto>();
        }
        public async Task<bool> PostExistsAsync(int postId)
        {
            string uri = $"{_basePath}/{postId}/?op=PostExists";
            var response = await _client.GetAsync(uri);
            return await response.ReadContentAs<bool>();
        }
    }
}
