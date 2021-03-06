using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WepAppTestPractices.MVC.Models
{
    public partial class WepAppTestPracticesDBContext : DbContext
    {
        public WepAppTestPracticesDBContext()
        {
        }

        public WepAppTestPracticesDBContext(DbContextOptions<WepAppTestPracticesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
