using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_net.Models
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Image> images { get; set; }
    }
}
