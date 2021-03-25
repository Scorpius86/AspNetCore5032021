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
            UsuarioPropietario = new UsuarioViewModel();
            UsuarioCreacion = new UsuarioViewModel();
            UsuarioActualizacion = new UsuarioViewModel();
        }

        public int PostId { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }

        public List<ComentarioViewModel> Comentarios { get; set; }
        public UsuarioViewModel UsuarioPropietario { get; set; }
        public UsuarioViewModel UsuarioCreacion { get; set; }
        public UsuarioViewModel UsuarioActualizacion { get; set; }
    }
}
