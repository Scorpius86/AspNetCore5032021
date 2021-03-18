using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.ApplicationService.Transient
{
    public class GuidTransientApplicationService:IGuidTransientApplicationService
    {
        private Guid _guidService { get; }

        public GuidTransientApplicationService()
        {
            _guidService = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guidService.ToString();
        }
    }
}
