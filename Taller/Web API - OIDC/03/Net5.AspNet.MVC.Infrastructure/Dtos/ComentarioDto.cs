using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Dtos
{
    public class ComentarioDto
    {
        public ComentarioDto()
        {
            Post = new PostDto();
            UsuarioPropietario = new UserDto();
            UsuarioCreacion = new UserDto();
            UsuarioActualizacion = new UserDto();
        }
        public int ComentarioId { get; set; }
        public int PostId { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioIdPropietario { get; set; }
        public string UsuarioIdCreacion { get; set; }
        public string UsuarioIdActualizacion { get; set; }

        public PostDto Post { get; set; }
        public UserDto UsuarioPropietario { get; set; }
        public UserDto UsuarioActualizacion { get; set; }
        public UserDto UsuarioCreacion { get; set; }
    }
}
