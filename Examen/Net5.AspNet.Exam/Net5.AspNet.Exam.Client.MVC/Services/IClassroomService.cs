using Net5.AspNet.Exam.Client.MVC.Models;
using System.Collections.Generic;

namespace Net5.AspNet.Exam.Client.MVC.Services
{
    public interface IClassroomService
    {
        StudentViewModel GetStudentById(int studentId);
        List<StudentViewModel> ListStudents();
        List<UserViewModel> ListFreeUsersByRole(string roleName, int? studenId = null);
        void InsertStudent(StudentViewModel studentViewModel);
        void UpdateStudent(StudentViewModel studentViewModel);
        void DeleteStudent(int studentId);
        bool StudentExists(int studentId);

        CourseViewModel GetCourseById(int courseId);
        List<CourseViewModel> ListCourses();
        void InsertCourse(CourseViewModel courseViewModel);
        void UpdateCourse(CourseViewModel courseViewModel);
        void DeleteCourse(int courseId);
        bool CourseExists(int courseId);

        List<GradeViewModel> ListGradesByStudentId(int studentId);
        List<CourseViewModel> ListFreeCoursesByStudent(int studentId);
        void InsertGrade(GradeViewModel gradeViewModel);
        GradeViewModel GetGradeById(int gradeId);
        void UpdateGrade(GradeViewModel gradeViewModel);
        void DeleteGrade(int gradeId);
        bool GradeExists(int gradeId);
    }
}