using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RybalkaApp.Models {
    public partial class tradeContext : DbContext {

        public static tradeContext Instance { get; set; } = new tradeContext();

        public tradeContext() {
        }

        public tradeContext(DbContextOptions<tradeContext> options)
            : base(options) {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderproduct> Orderproducts { get; set; } = null!;
        public virtual DbSet<Pickuppoint> Pickuppoints { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Productcategory> Productcategories { get; set; } = null!;
        public virtual DbSet<Productmanufacturer> Productmanufacturers { get; set; } = null!;
        public virtual DbSet<Productpostavshik> Productpostavshiks { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=trade;password=2232", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Order>(entity => {
                entity.ToTable("order");

                entity.HasIndex(e => e.OrderPickupPointId, "f1_idx");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderPickupPointId).HasColumnName("OrderPickupPointID");

                entity.Property(e => e.OrderStatus).HasColumnType("text");

                entity.HasOne(d => d.OrderPickupPoint)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderPickupPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f1");
            });

            modelBuilder.Entity<Orderproduct>(entity => {
                entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("orderproduct");

                entity.HasIndex(e => e.ProductArticleNumber, "ProductArticleNumber");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductArticleNumber)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderproduct_ibfk_1");

                entity.HasOne(d => d.ProductArticleNumberNavigation)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.ProductArticleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderproduct_ibfk_2");
            });

            modelBuilder.Entity<Pickuppoint>(entity => {
                entity.ToTable("pickuppoint");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.House).HasMaxLength(100);

                entity.Property(e => e.Street).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasKey(e => e.ProductArticleNumber)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.ProductCategoryId, "fk1_idx");

                entity.HasIndex(e => e.ProductManufacturerId, "fk2_idx");

                entity.HasIndex(e => e.ProductUnitId, "fk4_idx");

                entity.Property(e => e.ProductArticleNumber)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductDescription).HasColumnType("text");

                entity.Property(e => e.ProductManufacturerId).HasColumnName("ProductManufacturerID");

                entity.Property(e => e.ProductName).HasColumnType("text");

                entity.Property(e => e.ProductPhoto).HasMaxLength(100);

                entity.Property(e => e.ProductStatus).HasMaxLength(100);

                entity.Property(e => e.ProductUnitId).HasColumnName("ProductUnitID");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1");

                entity.HasOne(d => d.ProductManufacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2");

                entity.HasOne(d => d.ProductUnit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk3");
            });

            modelBuilder.Entity<Productcategory>(entity => {
                entity.ToTable("productcategory");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductCategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<Productmanufacturer>(entity => {
                entity.ToTable("productmanufacturer");

                entity.Property(e => e.ProductManufacturerId).HasColumnName("ProductManufacturerID");

                entity.Property(e => e.ProductManufacturerName).HasMaxLength(100);
            });

            modelBuilder.Entity<Productpostavshik>(entity => {
                entity.ToTable("productpostavshik");

                entity.Property(e => e.ProductPostavshikId).HasColumnName("ProductPostavshikID");

                entity.Property(e => e.ProductPostavshikName).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity => {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Unit>(entity => {
                entity.ToTable("unit");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserRole, "UserRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserLogin).HasColumnType("text");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserPassword).HasColumnType("text");

                entity.Property(e => e.UserPatronymic).HasMaxLength(100);

                entity.Property(e => e.UserSurname).HasMaxLength(100);

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
