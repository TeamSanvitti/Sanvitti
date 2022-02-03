using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { }

        public DbSet<svGeneralShipmentLabel> svGeneralShipmentLabel { get; set; }
        public DbSet<svShipBy> svShipBy { get; set; }

        

        // public DbSet<PackageType> PackageType { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        //              optionsBuilder.UseSqlServer("Server=3.18.221.3; Database=saavortest; User ID=bistrolive; Password=Lpci0@27;");
        //    }
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{          

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
