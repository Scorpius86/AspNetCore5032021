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
    public class GradesController : Controller
    {
        private readonly IClassroomService _classroomService;
        public GradesController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }
        [Authorize(Policy = Policies.GetGrades)]
        public IActionResult Index()
        {
            return View(_classroomService.ListStudents());
        }

        [Authorize(Policy = Policies.GetGrades)]
        public IActionResult List(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ListGradeViewModel listGradeViewModel = new ListGradeViewModel
            {
                Student = _classroomService.GetStudentById(id.Value),
                Grades = _classroomService.ListGradesByStudentId(id.Value),
                Courses = _classroomService.ListFreeCoursesByStudent(id.Value)
        };

            if (listGradeViewModel.Student == null)
            {
                return NotFound();
            }

            return View(listGradeViewModel);
        }
        [Authorize(Policy = Policies.AddGrades)]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GradeViewModel grade = new GradeViewModel();
            grade.Student = _classroomService.GetStudentById(id.Value);            
            grade.StudentId = grade.Student.StudentId;
            grade.Courses = _classroomService.ListFreeCoursesByStudent(id.Value);
            
            if (grade.Student == null)
            {
                return NotFound();
            }

            return View(grade);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.AddGrades)]
        public IActionResult Create([Bind("StudentId,CourseId,Value")] GradeViewModel gradeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _classroomService.InsertGrade(gradeViewModel);
                    return RedirectToAction(nameof(List), new { id = gradeViewModel.StudentId });
                }
                return RedirectToAction(nameof(List), new { id = gradeViewModel.StudentId });
            }
            catch (Exception ex)
            {
                gradeViewModel.Student = _classroomService.GetStudentById(gradeViewModel.StudentId);
                gradeViewModel.Courses = _classroomService.ListFreeCoursesByStudent(gradeViewModel.StudentId);

                return View(gradeViewModel);
                throw;
            }
        }
        [Authorize(Policy = Policies.GetGrades)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GradeViewModel gradeViewModel = _classroomService.GetGradeById(id.Value);
            if (gradeViewModel == null)
            {
                return NotFound();
            }

            gradeViewModel.Student = _classroomService.GetStudentById(gradeViewModel.StudentId);
            return View(gradeViewModel);
        }
        [Authorize(Policy = Policies.EditGrades)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GradeViewModel gradeViewModel = _classroomService.GetGradeById(id.Value);
            if (gradeViewModel == null)
            {
                return NotFound();
            }
            
            return View(gradeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.EditGrades)]
        public IActionResult Edit(int id, [Bind("GradeId,StudentId,CourseId,Value")] GradeViewModel gradeViewModel)
        {
            if (id != gradeViewModel.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _classroomService.UpdateGrade(gradeViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_classroomService.GradeExists(gradeViewModel.GradeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List), new { id = gradeViewModel.StudentId });
            }

            gradeViewModel = _classroomService.GetGradeById(id);
            
            return View(gradeViewModel);
        }
        [Authorize(Policy = Policies.DeleteGrades)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GradeViewModel gradeViewModel = _classroomService.GetGradeById(id.Value);
            if (gradeViewModel == null)
            {
                return NotFound();
            }

            return View(gradeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.DeleteGrades)]
        public IActionResult DeleteConfirmed(int id, [Bind("GradeId,StudentId,CourseId,Value")] GradeViewModel gradeViewModel)
        {
            _classroomService.DeleteGrade(id);
            return RedirectToAction(nameof(List), new { id = gradeViewModel.StudentId });
        }

    }
}
