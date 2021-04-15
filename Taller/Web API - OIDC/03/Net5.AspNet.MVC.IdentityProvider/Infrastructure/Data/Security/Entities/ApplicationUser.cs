using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.IdentityProvider.Infrastructure.Data.Security.Entities
{
    public class ApplicationUser : IdentityUser
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
    }
}
