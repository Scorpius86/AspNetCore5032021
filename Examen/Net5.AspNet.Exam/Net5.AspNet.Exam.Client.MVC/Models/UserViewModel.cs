using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Client.MVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
