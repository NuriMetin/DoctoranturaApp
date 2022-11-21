using Doctorantura.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctorantura.App.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Line> Lines { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<ColumnLine> ColumnsLines { get; set; }
        public DbSet<LineSum> LinesSum { get; set; }
        public DbSet<WLine> WLines { get; set; }
        public DbSet<AlfLine> AlfLines { get; set; }
        public DbSet<QamLine> QamLines { get; set; }
        public DbSet<XLine> XLines { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Column>().HasData(
            //    new Column { ID = 1, Name = "K1", Row = 1 },
            //    new Column { ID = 2, Name = "K2", Row = 2 },
            //    new Column { ID = 3, Name = "K3", Row = 3 },
            //    new Column { ID = 4, Name = "K4", Row = 4 },
            //    new Column { ID = 5, Name = "K5", Row = 5 },
            //    new Column { ID = 6, Name = "K6", Row = 6 },
            //    new Column { ID = 7, Name = "K7", Row = 7 },
            //    new Column { ID = 8, Name = "K8", Row = 8 }
            //    );

            //builder.Entity<Line>().HasData(
            //    new Line { ID = 1, Name = "K1", Row = 1 },
            //    new Line { ID = 2, Name = "K2", Row = 2 },
            //    new Line { ID = 3, Name = "K3", Row = 3 },
            //    new Line { ID = 4, Name = "K4", Row = 4 },
            //    new Line { ID = 5, Name = "K5", Row = 5 },
            //    new Line { ID = 6, Name = "K6", Row = 6 },
            //    new Line { ID = 7, Name = "K7", Row = 7 },
            //    new Line { ID = 8, Name = "K8", Row = 8 }
            //    );
        }
    }
}
