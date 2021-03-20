using Microsoft.AspNetCore.Mvc;
using Net5.Fundamentals.EF.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.EF.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            return View(_blogService.ListPosts());
        }
    }
}
