using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Models
{
    public class ListGradeViewModel
    {
        public ListGradeViewModel()
        {
            Grades = new List<GradeViewModel>();
        }
        public StudentViewModel Student { get; set; }
        public List<GradeViewModel> Grades { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
