using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Scoped
{
    public class GuidScopedApplicationService:IGuidScopedApplicationService
    {
        private Guid _guidService { get; }

        public GuidScopedApplicationService()
        {
            _guidService = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guidService.ToString();
        }
    }
}
