using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Net5.AspNet.Exam.Client.MVC.Models;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Entities;
using Net5.AspNet.Exam.Infrastructure.Data.Classroom.Repositories;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly ClassroomUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ClassroomService(
            ClassroomUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public List<StudentViewModel> ListStudents()
        {
            List<StudentViewModel> students = _mapper.Map<List<StudentViewModel>>(_unitOfWork.Students.ListStudentsWithGrades());
            List<string> studentsId = students.Select(s => s.UserId).ToList();
            List<ApplicationUser> users = _userManager.Users.Where(u => studentsId.Contains(u.Id)).ToList();
            students.ForEach(s =>
            {
                s.User = _mapper.Map<UserViewModel>(users.Where(u => u.Id == s.UserId).FirstOrDefault());
                s.GradesAverage = CalculateGradePointAverage(s.Grades);
            });

            return students;
        }
        public StudentViewModel GetStudentById(int studentId)
        {
            StudentViewModel student = _mapper.Map<StudentViewModel>(_unitOfWork.Students.ListStudentsWithGrades(s => s.StudentId == studentId).FirstOrDefault());
            ApplicationUser user = _userManager.Users.Where(u => u.Id == student.UserId).FirstOrDefault();
            student.User = _mapper.Map<UserViewModel>(user);
            student.GradesAverage = CalculateGradePointAverage(student.Grades);

            return student;
        }
        public List<UserViewModel> ListFreeUsersByRole(string roleName,int? studenId = null)
        {
            List<User> freeUsers = _unitOfWork.Users.ListFreeUser();
            List<ApplicationUser> studentUsers = _userManager.GetUsersInRoleAsync(roleName).Result.ToList();

            var query = from fu in freeUsers
                        join su in studentUsers on fu.Id equals su.Id
                        select fu;

            List<User> users = query.ToList();

            if (studenId.HasValue)
            {
                users.Add(_unitOfWork.Students.GetById(studenId.Value,"User").User);
            }

            return _mapper.Map<List<UserViewModel>>(users);
        }
        public void InsertStudent(StudentViewModel studentViewModel)
        {
            Student student = _mapper.Map<Student>(studentViewModel);

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            student.CreationUserId = userId;
            student.UpdateUserId = userId;
            student.UpdateDate = DateTime.Now;
            student.CreationDate = DateTime.Now;

            _unitOfWork.Students.Insert(student);
            _unitOfWork.Save();
        }
        public void UpdateStudent(StudentViewModel studentViewModel)
        {
            Student student = _unitOfWork.Students.GetById(studentViewModel.StudentId);

            student.FirstName = studentViewModel.FirstName;
            student.LastName = studentViewModel.LastName;
            student.SurName = studentViewModel.SurName;
            student.UserId = studentViewModel.UserId;

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);            
            student.UpdateUserId = userId;
            student.UpdateDate = DateTime.Now;

            _unitOfWork.Students.Update(student);
            _unitOfWork.Save();
        }
        public void DeleteStudent(int studentId)
        {
            Student student = _unitOfWork.Students.GetById(studentId);

            _unitOfWork.Students.Delete(student);
            _unitOfWork.Save();
        }
        public bool StudentExists(int studentId)
        {
            Student student = _unitOfWork.Students.GetById(studentId);
            return (student != null);
        }
        private Decimal CalculateGradePointAverage(List<GradeViewModel> grades)
        {
            return grades.Count > 0 ? Math.Round(grades.Average(g => g.Value),2) : 0;
        }


        public List<CourseViewModel> ListCourses()
        {
            List<CourseViewModel> courses = _mapper.Map<List<CourseViewModel>>(_unitOfWork.Courses.GetAll());
            
            return courses;
        }
        public CourseViewModel GetCourseById(int courseId)
        {
            CourseViewModel courseViewModel = _mapper.Map<CourseViewModel>(_unitOfWork.Courses.GetById(courseId));

            return courseViewModel;
        }
        public void InsertCourse(CourseViewModel courseViewModel)
        {
            Course course = _mapper.Map<Course>(courseViewModel);

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            course.CreationUserId = userId;
            course.UpdateUserId = userId;
            course.UpdateDate = DateTime.Now;
            course.CreationDate = DateTime.Now;

            _unitOfWork.Courses.Insert(course);
            _unitOfWork.Save();
        }
        public void UpdateCourse(CourseViewModel courseViewModel)
        {
            Course course = _unitOfWork.Courses.GetById(courseViewModel.CourseId);

            course.Description = courseViewModel.Description;            
            
            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            course.UpdateUserId = userId;
            course.UpdateDate = DateTime.Now;

            _unitOfWork.Courses.Update(course);
            _unitOfWork.Save();
        }
        public void DeleteCourse(int courseId)
        {
            Course course = _unitOfWork.Courses.GetById(courseId);

            _unitOfWork.Courses.Delete(course);
            _unitOfWork.Save();
        }
        public bool CourseExists(int courseId)
        {
            Course course = _unitOfWork.Courses.GetById(courseId);
            return (course != null);
        }


        public List<GradeViewModel> ListGradesByStudentId(int studentId)
        {
            List<GradeViewModel> gradeViewModels = _mapper.Map<List<GradeViewModel>>(_unitOfWork.Grades.GetAll(g=>g.StudentId == studentId, includeProperties: "Student,Course"));
            
            return gradeViewModels;
        }
        public GradeViewModel GetGradeById(int gradeId)
        {
            GradeViewModel grade = _mapper.Map<GradeViewModel>(_unitOfWork.Grades.GetAll(s => s.GradeId == gradeId, includeProperties: "Student,Course").FirstOrDefault());            
            
            return grade;
        }
        public List<CourseViewModel> ListFreeCoursesByStudent(int studentId)
        {
            List<Course> freeCourses = _unitOfWork.Courses.ListFreeCourse(studentId);
            
            return _mapper.Map<List<CourseViewModel>>(freeCourses);
        }
        public void InsertGrade(GradeViewModel gradeViewModel)
        {
            Grade grade = _mapper.Map<Grade>(gradeViewModel);

            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            grade.CreationUserId = userId;
            grade.UpdateUserId = userId;
            grade.UpdateDate = DateTime.Now;
            grade.CreationDate = DateTime.Now;

            _unitOfWork.Grades.Insert(grade);
            _unitOfWork.Save();
        }
        public void UpdateGrade(GradeViewModel gradeViewModel)
        {
            Grade grade = _unitOfWork.Grades.GetById(gradeViewModel.GradeId);

            grade.Value = gradeViewModel.Value;
            
            string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            grade.UpdateUserId = userId;
            grade.UpdateDate = DateTime.Now;

            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Save();
        }
        public void DeleteGrade(int gradeId)
        {
            Grade grade = _unitOfWork.Grades.GetById(gradeId);

            _unitOfWork.Grades.Delete(grade);
            _unitOfWork.Save();
        }
        public bool GradeExists(int gradeId)
        {
            Grade grade = _unitOfWork.Grades.GetById(gradeId);
            return (grade != null);
        }
    }
}
