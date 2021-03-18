using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Singleton
{
    public class GuidSingletonApplicationService:IGuidSingletonApplicationService
    {
        private Guid _guidService { get; }

        public GuidSingletonApplicationService()
        {
            _guidService = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guidService.ToString();
        }
    }
}
