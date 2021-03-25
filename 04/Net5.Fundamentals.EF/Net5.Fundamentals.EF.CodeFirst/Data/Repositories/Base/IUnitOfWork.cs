using System;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base
{
    public interface IUnitOfWork:IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IPostRepository Posts { get; }
        IComentarioRepository Comentarios { get; }
        void Save();
    }
}