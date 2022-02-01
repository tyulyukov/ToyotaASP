using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Toyota.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Entities.Color> Colors { get; set; }
        public DbSet<Entities.Modification> Modifications { get; set; }
        public DbSet<Entities.Model> Models { get; set; }
        public DbSet<Entities.ModificationColors> ModificationColors { get; set; }
        public DbSet<Entities.CallBack> CallBacks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entities.CallBack>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
