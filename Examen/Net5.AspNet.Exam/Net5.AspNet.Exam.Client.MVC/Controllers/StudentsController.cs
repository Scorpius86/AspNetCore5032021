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
    public class StudentsController : Controller
    {
        private readonly IClassroomService _classroomService;
        public StudentsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }
        [Authorize(Policy =Policies.GetStudents)]
        public IActionResult Index()
        {
            return View(_classroomService.ListStudents());
        }

        [Authorize(Policy = Policies.AddStudents)]
        public IActionResult Create()
        {
            StudentViewModel student = new StudentViewModel();
            student.Users = _classroomService.ListFreeUsersByRole(Roles.Student);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.AddStudents)]
        public IActionResult Create([Bind("UserId,FirstName,LastName,SurName")] StudentViewModel studentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _classroomService.InsertStudent(studentViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                studentViewModel.Users = _classroomService.ListFreeUsersByRole(Roles.Student);
                return View(studentViewModel);
                throw;
            }
        }

        [Authorize(Policy = Policies.GetStudents)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentViewModel studentViewModel = _classroomService.GetStudentById(id.Value);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        [Authorize(Policy = Policies.EditStudents)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentViewModel studentViewModel = _classroomService.GetStudentById(id.Value);
            studentViewModel.Users = _classroomService.ListFreeUsersByRole(Roles.Student, id.Value);

            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.EditStudents)]
        public IActionResult Edit(int id, [Bind("StudentId,UserId,FirstName,LastName,SurName")] StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _classroomService.UpdateStudent(studentViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_classroomService.StudentExists(studentViewModel.StudentId))
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

            studentViewModel.Users = _classroomService.ListFreeUsersByRole(Roles.Student, id);

            return View(studentViewModel);
        }

        [Authorize(Policy = Policies.DeleteStudents)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentViewModel studentViewModel = _classroomService.GetStudentById(id.Value);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        [HttpPost, ActionName("Delete")]        
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.DeleteStudents)]
        public IActionResult DeleteConfirmed(int id)
        {
            _classroomService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
