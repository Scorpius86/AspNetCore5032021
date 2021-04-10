using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5.AspNet.Exam.Client.MVC.Models;
using Net5.AspNet.Exam.Client.MVC.Services;
using Net5.AspNet.Exam.Infrastructure.Audit;
using Net5.AspNet.Exam.Infrastructure.Log;
using Net5.AspNet.Exam.Infrastructure.Security.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    [Audit]
    public class CoursesController : Controller
    {
        private readonly IClassroomService _classroomService;
        public CoursesController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }
        [Authorize(Policy = Policies.GetCourses)]
        public IActionResult Index()
        {
            return View(_classroomService.ListCourses());
        }
        [Authorize(Policy = Policies.AddCourses)]
        public IActionResult Create()
        {
            CourseViewModel courseViewModel = new CourseViewModel();            
            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.AddCourses)]
        public IActionResult Create([Bind("Description")] CourseViewModel courseViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _classroomService.InsertCourse(courseViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {                
                return View(courseViewModel);
                throw;
            }
        }

        [Authorize(Policy = Policies.GetCourses)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = _classroomService.GetCourseById(id.Value);
            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        [Authorize(Policy = Policies.EditCourses)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = _classroomService.GetCourseById(id.Value);
            
            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.EditCourses)]
        public IActionResult Edit(int id, [Bind("CourseId,Description")] CourseViewModel courseViewModel)
        {
            if (id != courseViewModel.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _classroomService.UpdateCourse(courseViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_classroomService.CourseExists(courseViewModel.CourseId))
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

            return View(courseViewModel);
        }
        [Authorize(Policy = Policies.DeleteCourses)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseViewModel courseViewModel = _classroomService.GetCourseById(id.Value);
            if (courseViewModel == null)
            {
                return NotFound();
            }

            return View(courseViewModel);
        }

        [HttpPost, ActionName("Delete")]        
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.DeleteCourses)]
        public IActionResult DeleteConfirmed(int id)
        {
            _classroomService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
