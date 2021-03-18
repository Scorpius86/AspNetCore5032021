using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Core
{
    public interface ICoreApplicationService
    {
        string GetGuidTransient();
        string GetGuidScoped();
        string GetGuidSingleton();
    }
}
