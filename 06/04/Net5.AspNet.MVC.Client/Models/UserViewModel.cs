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
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
    }
}
