using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Models
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Grades = new List<GradeViewModel>();
        }

        [DisplayName("Student Id")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "The User field is required.")]
        [DisplayName("User")]
        public string UserId { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Sur Name")]
        [Required]
        public string SurName { get; set; }
        [DisplayName("Student Full Name")]
        public string FullName { get; set; }
        public string CreationUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        [DisplayName("Grades Average")]
        public Decimal GradesAverage { get; set; }

        public virtual List<GradeViewModel> Grades { get; set; }
        public virtual UserViewModel User { get; set; }

        public virtual List<UserViewModel> Users { get; set; }
    }
}
