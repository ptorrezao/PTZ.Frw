using Microsoft.EntityFrameworkCore;
using PTZ.Frw.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTZ.Frw.DataAccess.Services
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
