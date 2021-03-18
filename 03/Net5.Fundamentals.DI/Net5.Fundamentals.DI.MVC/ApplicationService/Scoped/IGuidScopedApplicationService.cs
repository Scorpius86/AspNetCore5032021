using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Scoped
{
    public interface IGuidScopedApplicationService
    {
        string GetGuid();
    }
}
