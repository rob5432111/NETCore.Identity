using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Identity.API.Resources
{
    public class UserSignUpResource
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
