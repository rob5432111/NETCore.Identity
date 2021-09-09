using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETCore.Identity.Core.Models;
using System;

namespace NETCore.Identity.Data
{
    public class IdentityDBContext : IdentityDbContext<User, Role, int>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
