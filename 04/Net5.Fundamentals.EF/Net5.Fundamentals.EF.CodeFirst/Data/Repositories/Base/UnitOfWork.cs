using Net5.Fundamentals.EF.CodeFirst.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Fundamentals.EF.CodeFirst.Data.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsuarioRepository Usuarios { get; }
        public IComentarioRepository Comentarios { get; }
        public IPostRepository Posts { get; }

        private Net5FundamentalsEFDatabaseContext _context;

        public UnitOfWork(
            Net5FundamentalsEFDatabaseContext context,
            IUsuarioRepository usuarioRepository,
            IComentarioRepository comentarioRepository,
            IPostRepository postRepository
        )
        {
            _context = context;
            Usuarios = usuarioRepository;
            Comentarios = comentarioRepository;
            Posts = postRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
