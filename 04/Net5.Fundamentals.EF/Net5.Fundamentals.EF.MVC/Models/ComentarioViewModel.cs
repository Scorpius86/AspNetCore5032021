﻿using System;

namespace Net5.Fundamentals.EF.MVC.Models
{
    public class ComentarioViewModel
    {
        public ComentarioViewModel()
        {
            Post = new PostViewModel();
            UsuarioPropietario = new UsuarioViewModel();
            UsuarioCreacion = new UsuarioViewModel();
            UsuarioActualizacion = new UsuarioViewModel();
        }
        public int ComentarioId { get; set; }
        public int PostId { get; set; }
        public string Contenido { get; set; }        
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaActualizacion { get; set; }        
        public int UsuarioIdPropietario { get; set; }
        public int UsuarioIdCreacion { get; set; }
        public int UsuarioIdActualizacion { get; set; }

        public PostViewModel Post { get; set; }
        public UsuarioViewModel UsuarioPropietario { get; set; }
        public UsuarioViewModel UsuarioCreacion { get; set; }
        public UsuarioViewModel UsuarioActualizacion { get; set; }

    }
}