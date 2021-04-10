using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Models
{
    public class CourseViewModel
    {
        [DisplayName("Course Id")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [DisplayName("Course")]
        public string Description { get; set; }
        public string CreationUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
