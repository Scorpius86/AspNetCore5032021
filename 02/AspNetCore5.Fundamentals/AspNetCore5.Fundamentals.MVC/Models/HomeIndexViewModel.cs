using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore5.Fundamentals.MVC.Models
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public List<PersonViewModel> People { get; set; }
        public List<string> Districts { get; set; }
    }
}
