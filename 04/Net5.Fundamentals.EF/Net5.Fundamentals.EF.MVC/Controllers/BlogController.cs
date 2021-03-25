﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5.Fundamentals.EF.MVC.Models;
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
                
        public ActionResult CreatePost()
        {            
            return View();
        }

        [HttpPost]
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
                return View(postViewModel);
            }
        }
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
        [ValidateAntiForgeryToken]
        public IActionResult DeletePostConfirmed(int id)
        {
            _blogService.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
