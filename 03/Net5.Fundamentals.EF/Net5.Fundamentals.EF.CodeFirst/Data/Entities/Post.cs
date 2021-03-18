﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Net5.Fundamentals.EF.CodeFirst.Data.Entities
{
    public partial class Post
    {
        public Post()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public int PostId { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int UsuarioIdPropietario { get; set; }
        public int UsuarioIdCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioIdActualizacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public virtual Usuario UsuarioIdActualizacionNavigation { get; set; }
        public virtual Usuario UsuarioIdCreacionNavigation { get; set; }
        public virtual Usuario UsuarioIdPropietarioNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}