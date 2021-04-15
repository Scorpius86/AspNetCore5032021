using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Constants
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string PowerUser = "PowerUser";
        public const string User = "User";
        public const string AdministratorOrPowerUser = Administrator + "," + PowerUser;
    }
}
