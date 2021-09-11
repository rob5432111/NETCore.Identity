using NETCore.Identity.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.Identity.Core.Services
{
    public interface IJwtService
    {
        string GenerateJwt(User user, IList<string> roles, string secret, string issuer, int expirationInDays);
    }
}
