using Microsoft.AspNetCore.Mvc;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using Net5.AspNet.MVC.Blog.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Blog.API.Controllers
{
    [Route("api/Posts/{postId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public CommentsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        
        [HttpPost]        
        public ComentarioDto InsertComment(int postId, ComentarioDto comentarioDto)
        {
            return _blogService.InsertComment(comentarioDto);
        }                
    }
}
