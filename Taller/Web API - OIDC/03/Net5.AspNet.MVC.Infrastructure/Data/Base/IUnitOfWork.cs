using System;

namespace Net5.AspNet.MVC.Infrastructure.Data.Base
{
    public interface IUnitOfWork<DBContext>:IDisposable
    {
        void Save();
    }
}