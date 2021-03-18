using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Transient
{
    public interface IGuidTransientApplicationService
    {
        string GetGuid();
    }
}
