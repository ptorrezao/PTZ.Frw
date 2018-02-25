using Microsoft.EntityFrameworkCore;
using PTZ.Frw.DataAccess.Models;

namespace PTZ.Frw.DataAccess
{
    public class PTZFrwContext: DbContext
    {
        public PTZFrwContext(DbContextOptions<PTZFrwContext> options)
            : base (options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Details)
                .WithOne(b => b.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}
