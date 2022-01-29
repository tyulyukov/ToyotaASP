using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toyota.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Entities.Color> Colors { get; set; }
        public DbSet<Entities.Modification> Modifications { get; set; }
        public DbSet<Entities.Model> Models { get; set; }
        public DbSet<Entities.ModificationColors> ModificationColors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
