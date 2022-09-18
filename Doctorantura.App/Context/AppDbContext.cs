using Doctorantura.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctorantura.App.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<LineNum> LineNums { get; set; }
        public DbSet<ColumnNum> ColumnNums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ColumnNum>().HasData(
                new ColumnNum { ID = 1, Name = "K1", Value = 1 },
                new ColumnNum { ID = 2, Name = "K2", Value = 2 },
                new ColumnNum { ID = 3, Name = "K3", Value = 3 },
                new ColumnNum { ID = 4, Name = "K4", Value = 3 },
                new ColumnNum { ID = 5, Name = "K5", Value = 5 },
                new ColumnNum { ID = 6, Name = "K6", Value = 7 },
                new ColumnNum { ID = 7, Name = "K7", Value = 9 },
                new ColumnNum { ID = 8, Name = "K8", Value = 9 }
                );

            //builder.Entity<LineNum>().HasData(
            //    new LineNum { ID = 1, Name = "K1", Value = 1 },
            //    new LineNum { ID = 2, Name = "K2", Value = 2 },
            //    new LineNum { ID = 3, Name = "K3", Value = 3 },
            //    new LineNum { ID = 4, Name = "K4", Value = 3 },
            //    new LineNum { ID = 5, Name = "K5", Value = 5 },
            //    new LineNum { ID = 6, Name = "K6", Value = 7 },
            //    new LineNum { ID = 7, Name = "K7", Value = 9 },
            //    new LineNum { ID = 8, Name = "K8", Value = 9 }
            //    );
        }
    }
}
