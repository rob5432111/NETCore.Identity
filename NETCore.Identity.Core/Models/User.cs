using Microsoft.AspNetCore.Identity;
using System;

namespace NETCore.Identity.Core.Models
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
