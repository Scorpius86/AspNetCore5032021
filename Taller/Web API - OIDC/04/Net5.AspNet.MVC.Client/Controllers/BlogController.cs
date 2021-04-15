using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.MVC.Client.Helper;
using Net5.AspNet.MVC.Client.Models;
using Net5.AspNet.MVC.Infrastructure.Agents.Comments;
using Net5.AspNet.MVC.Infrastructure.Agents.Posts;
using Net5.AspNet.MVC.Infrastructure.Constants;
using Net5.AspNet.MVC.Infrastructure.Dtos;
using Net5.AspNet.MVC.Infrastructure.Helper.Audit;
using Net5.AspNet.MVC.Infrastructure.Helper.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    [Audit]
    public class BlogController : Controller
    {
        private readonly IPostAgent _postAgent;
        private readonly ICommentsAgent _comentariosAgent;
        private readonly IMapper _mapper;
        public BlogController(IPostAgent postAgent, ICommentsAgent comentariosAgent,IMapper mapper)
        {
            _postAgent = postAgent;
            _comentariosAgent = comentariosAgent;
            _mapper = mapper;
        }        
        public async Task<ActionResult> Index()
        {            
            return View( _mapper.Map<List<PostViewModel>>(await _postAgent.ListPostsAsync()));
        }
        public async Task<ActionResult> PostDetails(int id)
        {
            return View(_mapper.Map<PostViewModel>(await _postAgent.GetPostByIdAsync(id)));
        }                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateComment([Bind("PostId,Contenido")] ComentarioViewModel comentarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _comentariosAgent.InsertCommentsAsync(comentarioViewModel.PostId,_mapper.Map<ComentarioDto>(comentarioViewModel));
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
        public async Task<ActionResult> CreatePost([Bind("Titulo,Resumen,Contenido")] PostViewModel postViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _postAgent.InsertPostAsync(_mapper.Map<PostDto>(postViewModel));
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
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostViewModel postViewModel = _mapper.Map<PostViewModel>(await _postAgent.GetPostByIdAsync(id.Value));
            if (postViewModel == null)
            {
                return NotFound();
            }
            
            return View(postViewModel);
        }
        [HttpPost]
        [Authorize(Policy = Policies.EditPost)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("PostId,Titulo,Resumen,Contenido")] PostViewModel postViewModel)
        {
            if (id != postViewModel.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postAgent.UpdatePostAsync(id,_mapper.Map<PostDto>(postViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _postAgent.PostExistsAsync(postViewModel.PostId))
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
        public async System.Threading.Tasks.Task<IActionResult> DeletePostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostViewModel postViewModel = _mapper.Map<PostViewModel>(await _postAgent.GetPostByIdAsync(id.Value));
            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }
        [HttpPost, ActionName("DeletePost")]
        [Authorize(Policy = Policies.DeletePost)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostConfirmedAsync(int id)
        {
            await _postAgent.DeletePostAsync(id);
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
