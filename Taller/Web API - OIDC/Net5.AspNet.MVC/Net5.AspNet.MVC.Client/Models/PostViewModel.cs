using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Models
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Comentarios = new List<ComentarioViewModel>();
            UsuarioPropietario = new UserViewModel();
            UsuarioCreacion = new UserViewModel();
            UsuarioActualizacion = new UserViewModel();
        }

        public int PostId { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }

        public List<ComentarioViewModel> Comentarios { get; set; }
        public UserViewModel UsuarioPropietario { get; set; }
        public UserViewModel UsuarioCreacion { get; set; }
        public UserViewModel UsuarioActualizacion { get; set; }
    }
}
