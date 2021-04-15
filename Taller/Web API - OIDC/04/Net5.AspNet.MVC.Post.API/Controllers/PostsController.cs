using Microsoft.AspNetCore.Mvc;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using Net5.AspNet.MVC.Blog.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net5.AspNet.MVC.Infrastructure.Helper.Audit;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;

namespace Net5.AspNet.MVC.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogFilter))]
    [Audit]
    public class PostsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public PostsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public IActionResult ListPosts()
        {
            return Ok(_blogService.ListPosts());
        }
        [HttpGet("{postId}")]
        public IActionResult GetPostById(int postId, [FromQuery] string op)
        {
            if (op == "PostExists")
            {
                return Ok(_blogService.PostExists(postId));
            }
            else
            {
                return Ok(_blogService.GetPostById(postId));
            }            
        }        
        [HttpPost]
        public IActionResult InsertPost(PostDto postDto)
        {
            return Ok(_blogService.InsertPost(postDto));
        }

        [HttpPut("{postId}")]
        public IActionResult UpdatePost(int postId, PostDto postDto)
        {
            return Ok(_blogService.UpdatePost(postDto));
        }
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {
            return Ok(_blogService.DeletePost(postId));
        }
    }
}
