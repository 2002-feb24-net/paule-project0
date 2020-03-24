using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseTargets.lib.Model
{
    public partial class PersonDbContext : DbContext
    {
        public PersonDbContext()
        {
        }

        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GeneralStock> GeneralStock { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<OrderStock> OrderStock { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<StoreStock> StoreStock { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);           
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralStock>(entity =>
            {
                entity.HasKey(e => e.StockId)
                    .HasName("PK__GeneralS__2C83A9E272A1615A");

                entity.HasIndex(e => e.StockName)
                    .HasName("UQ__GeneralS__31563119DF0C70E0")
                    .IsUnique();

                entity.Property(e => e.StockId)
                    .HasColumnName("StockID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderStockId).HasColumnName("OrderStockID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StockDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StockName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StoreStockId).HasColumnName("StoreStockID");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.HasOne(d => d.OrderStock)
                    .WithMany(p => p.GeneralStock)
                    .HasForeignKey(d => d.OrderStockId)
                    .HasConstraintName("FK__GeneralSt__Order__6166761E");

                entity.HasOne(d => d.StoreStock)
                    .WithMany(p => p.GeneralStock)
                    .HasForeignKey(d => d.StoreStockId)
                    .HasConstraintName("FK__GeneralSt__Store__625A9A57");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.GeneralStock)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GeneralSt__Topic__607251E5");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Location__E7FEA4778440CD16");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderStock>(entity =>
            {
                entity.Property(e => e.OrderStockId)
                    .HasColumnName("OrderStockID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStock)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderStoc__Order__59C55456");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BAFE2574A6D");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Orders__PersonID__55009F39");
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__People__AA2FFB858CFA1AA4");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__People__536C85E4177F94E5")
                    .IsUnique();

                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(26)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreStock>(entity =>
            {
                entity.Property(e => e.StoreStockId)
                    .HasColumnName("StoreStockID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.StoreStock)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreStoc__Locat__5CA1C101");
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.HasKey(e => e.TopicId)
                    .HasName("PK__Topics__022E0F7D59A24CF4");

                entity.Property(e => e.TopicId)
                    .HasColumnName("TopicID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TopicName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
