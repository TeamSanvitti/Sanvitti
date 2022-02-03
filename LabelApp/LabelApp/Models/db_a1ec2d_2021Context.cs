using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LabelApp.Models
{
    public partial class db_a1ec2d_2021Context : DbContext
    {
        public db_a1ec2d_2021Context()
        {
        }

        public db_a1ec2d_2021Context(DbContextOptions<db_a1ec2d_2021Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=sql5103.site4now.net;database=db_a1ec2d_2021;User ID=db_a1ec2d_2021_admin;PWD=Test@1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.HasSequence<int>("AuthorizationSequence").StartsAt(100001);

            modelBuilder.HasSequence<int>("fileSequence").StartsAt(100001);

            modelBuilder.HasSequence<int>("seq_TestVarcharSequenceNumber");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
