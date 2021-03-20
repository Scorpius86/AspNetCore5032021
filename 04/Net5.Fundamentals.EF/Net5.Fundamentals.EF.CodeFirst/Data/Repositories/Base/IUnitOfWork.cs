using System;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base
{
    public interface IUnitOfWork:IDisposable
    {
        IComentarioRepository Comentarios { get; }
        IPostRepository Posts { get; }
        IUsuarioRepository Usuarios { get; }

        void Save();
    }
}