using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataSinkProject
{
    public partial class DataSinkDbContext : DbContext
    {
        public DataSinkDbContext()
        {
        }

        public DataSinkDbContext(DbContextOptions<DataSinkDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Datum> Datum { get; set; }
        public virtual DbSet<DatumAttrib> DatumAttrib { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=DataSinkDB;User Id=SA;Password=Password1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Datum>(entity =>
            {
                entity.HasKey(e => e.PkDatumId)
                    .HasName("PK__Datum__4922022D6F4879F8");
            });

            modelBuilder.Entity<DatumAttrib>(entity =>
            {
                entity.HasKey(e => e.PkDatumAttribId)
                    .HasName("PK__DatumAtt__98EF977EE7A29E09");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkDatum)
                    .WithMany(p => p.DatumAttrib)
                    .HasForeignKey(d => d.FkDatumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DatumAttr__FkDat__38996AB5");
            });
        }
    }
}
