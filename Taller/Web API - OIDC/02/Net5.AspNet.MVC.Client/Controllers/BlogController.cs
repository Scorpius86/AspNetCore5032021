using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.MVC.Client.Helper;
using Net5.AspNet.MVC.Client.Models;
using Net5.AspNet.MVC.Client.Services;
using Net5.AspNet.MVC.Infrastructure.Constants;
using Net5.AspNet.MVC.Infrastructure.Helper.Audit;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;
using System;
using System.Diagnostics;

namespace Net5.AspNet.MVC.Client.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    [Audit]    
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }        
        public ActionResult Index()
        {            
            return View(_blogService.ListPosts());
        }
        public ActionResult PostDetails(int id)
        {
            return View(_blogService.GetPostById(id));
        }                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind("PostId,Contenido")] ComentarioViewModel comentarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _blogService.InsertComment(comentarioViewModel);                    
                    return RedirectToAction("PostDetails", "Blog", new { id = comentarioViewModel.PostId });
                }
                return RedirectToAction("PostDetails", "Blog", new { id = comentarioViewModel.PostId });
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
                
        [Authorize(Roles = Roles.AdministratorOrPowerUser)]
        public ActionResult CreatePost()
        {            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.AdministratorOrPowerUser)]        
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind("Titulo,Resumen,Contenido")] PostViewModel postViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _blogService.InsertPost(postViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //return View(postViewModel);
                throw;
            }
        }

        [Authorize(Policy = Policies.EditPost)]
        public IActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostViewModel postViewModel = _blogService.GetPostById(id.Value);
            if (postViewModel == null)
            {
                return NotFound();
            }
            
            return View(postViewModel);
        }
        [HttpPost]
        [Authorize(Policy = Policies.EditPost)]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(int id, [Bind("PostId,Titulo,Resumen,Contenido")] PostViewModel postViewModel)
        {
            if (id != postViewModel.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogService.UpdatePost(postViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blogService.PostExists(postViewModel.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(postViewModel);
        }

        [Authorize(Policy = Policies.DeletePost)]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostViewModel postViewModel = _blogService.GetPostById(id.Value);
            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }
        [HttpPost, ActionName("DeletePost")]
        [Authorize(Policy = Policies.DeletePost)]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePostConfirmed(int id)
        {
            _blogService.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Errorpage()
        {
            return View();
        }
    }
}
