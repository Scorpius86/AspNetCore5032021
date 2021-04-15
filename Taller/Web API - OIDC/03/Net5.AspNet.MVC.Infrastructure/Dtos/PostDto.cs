using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Dtos
{
    public class PostDto
    {
        public PostDto()
        {
            Comentarios = new List<ComentarioDto>();
            UsuarioPropietario = new UserDto();
            UsuarioCreacion = new UserDto();
            UsuarioActualizacion = new UserDto();
        }

        public int PostId { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }

        public List<ComentarioDto> Comentarios { get; set; }
        public UserDto UsuarioPropietario { get; set; }
        public UserDto UsuarioCreacion { get; set; }
        public UserDto UsuarioActualizacion { get; set; }
    }
}
