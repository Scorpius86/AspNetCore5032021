using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public string UsuarioId { get; set; }
        public string UserName { get; set; }
        public string Clave { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string FullName { get; set; }
    }
}
