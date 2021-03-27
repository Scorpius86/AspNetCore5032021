using System;

namespace Net5.AspNet.MVC.Infrastructure.Data.Repositories.Base
{
    public interface IUnitOfWork:IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IPostRepository Posts { get; }
        IComentarioRepository Comentarios { get; }
        void Save();
    }
}