using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RetroFront.Models;

namespace RetroFront.DataAccess
{
    public class AppSQLIteContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Emulator> Emulators { get; set; }
        public DbSet<Systeme> Systemes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=%HOMEPATH%\.retrofront\retrofront.db");
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
