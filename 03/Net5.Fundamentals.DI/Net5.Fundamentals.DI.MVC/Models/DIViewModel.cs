using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.Fundamentals.DI.MVC.Models
{
    public class DIViewModel
    {
        public DIViewModel()
        {
            GuidTransient = new GuidViewModel();
            GuidScoped = new GuidViewModel();
            GuidSingleton = new GuidViewModel();
        }
        public GuidViewModel GuidTransient { get; set; }
        public GuidViewModel GuidScoped { get; set; }
        public GuidViewModel GuidSingleton { get; set; }
    }
}
