using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Models
{
    public class GradeViewModel
    {
        [DisplayName("Grade Id")]
        public int GradeId { get; set; }
        [DisplayName("Grade")]
        [Required]
        public decimal Value { get; set; }        
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string CreationUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual StudentViewModel Student { get; set; }
        public virtual CourseViewModel Course { get; set; }

        public virtual List<CourseViewModel> Courses { get; set; }
    }
}
