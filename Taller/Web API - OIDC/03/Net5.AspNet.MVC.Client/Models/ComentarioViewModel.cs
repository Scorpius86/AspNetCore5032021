using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Models
{
    public class ComentarioViewModel
    {
        public ComentarioViewModel()
        {
            Post = new PostViewModel();
            UsuarioPropietario = new UserViewModel();
            UsuarioCreacion = new UserViewModel();
            UsuarioActualizacion = new UserViewModel();
        }
        public int ComentarioId { get; set; }
        public int PostId { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioIdPropietario { get; set; }
        public string UsuarioIdCreacion { get; set; }
        public string UsuarioIdActualizacion { get; set; }

        public PostViewModel Post { get; set; }
        public UserViewModel UsuarioPropietario { get; set; }
        public UserViewModel UsuarioActualizacion { get; set; }
        public UserViewModel UsuarioCreacion { get; set; }
    }
}
