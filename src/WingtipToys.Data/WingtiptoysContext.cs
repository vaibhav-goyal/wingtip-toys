using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WingtipToys.Domain.Models;

namespace WingtipToys.Data
{
    public partial class WingtiptoysContext : DbContext
    {
        public WingtiptoysContext()
        {
        }

        public WingtiptoysContext(DbContextOptions<WingtiptoysContext> options)
            : base(options)
        {
        }        
        public virtual DbSet<Category> Categories { get; set; }       
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Wingtiptoys;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_dbo.Categories");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
         
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_dbo.Products");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryID");
            });
        }
    }
}
