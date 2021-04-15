using Net5.AspNet.MVC.Infrastructure.Dtos;
using Net5.AspNet.MVC.Infrastructure.Helper.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Agents.Comments
{
    public class CommentsAgent : ICommentsAgent
    {
        private readonly HttpClient _client;
        private readonly string _basePath = "/api/Posts/{0}/Comments";

        public CommentsAgent(HttpClient client)
        {
            _client = client;
        }
        public async Task<ComentarioDto> InsertCommentsAsync(int postId, ComentarioDto comentarioDto)
        {
            string uri = $"{string.Format(_basePath, postId)}";
            var response = await _client.PostAsJsonAsync(uri, comentarioDto);
            return await response.ReadContentAs<ComentarioDto>();
        }
    }
}
