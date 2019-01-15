using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yozian.EFCorePlus.Extensions;
using Yozian.EFCorePlus.Loggers;
using Yozian.EFCorePlus.Test.Data.Entities;

namespace Yozian.EFCorePlus.Test.Data
{
    public class SouthSeaDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase("MyTestDb");
            var ct = optionsBuilder.Options.ContextType;

            // add logger (InMemoryDatabase dosen't support)

            // optionsBuilder.AddExecutedCommandConsoleLogger();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
