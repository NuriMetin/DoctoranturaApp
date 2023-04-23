using Doctorantura.App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Doctorantura.App.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
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
        public DbSet<Gender> Genders { get; set; }
        public DbSet<CalcTask> CalcTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ColumnLine>()
             .HasOne(x => x.CalcTask)
            .WithMany(x => x.ColumnLines)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ColumnLine>()
            .HasOne(x => x.Column)
           .WithMany(x => x.ColumnLines)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ColumnLine>()
            .HasOne(x => x.Line)
           .WithMany(x => x.ColumnsLines)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
