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
    }

}
