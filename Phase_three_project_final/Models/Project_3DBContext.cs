using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phase_three_project_final.Models
{
    public partial class Project_3DBContext : DbContext
    {
        public Project_3DBContext()
        {
        }

        public Project_3DBContext(DbContextOptions<Project_3DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmin { get; set; }
        public virtual DbSet<TblCustomers> TblCustomers { get; set; }
        public virtual DbSet<TblLaptops> TblLaptops { get; set; }
        public virtual DbSet<TblVendors> TblVendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=(local);integrated security =true;database=Project_3DB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__tbl_admi__43AA41416E07878D");

                entity.ToTable("tbl_admin");

                entity.Property(e => e.AdminId)
                    .HasColumnName("admin_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdminPwd)
                    .HasColumnName("admin_pwd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AdminUname)
                    .HasColumnName("admin_uname")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCustomers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__tbl_cust__CD65CB85EF9EEB77");

                entity.ToTable("tbl_customers");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerEmail)
                    .HasColumnName("customer_email")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPwd)
                    .HasColumnName("customer_pwd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerUname)
                    .HasColumnName("customer_uname")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLaptops>(entity =>
            {
                entity.HasKey(e => e.LaptopId)
                    .HasName("PK__tbl_lapt__BFA247ACECC6ACA3");

                entity.ToTable("tbl_laptops");

                entity.Property(e => e.LaptopId)
                    .HasColumnName("laptop_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LaptopImage)
                    .HasColumnName("laptop_image")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LaptopName)
                    .HasColumnName("laptop_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LaptopPrice).HasColumnName("laptop_price");
            });

            modelBuilder.Entity<TblVendors>(entity =>
            {
                entity.HasKey(e => e.VendorId)
                    .HasName("PK__tbl_vend__0F7D2B786D123E54");

                entity.ToTable("tbl_vendors");

                entity.Property(e => e.VendorId)
                    .HasColumnName("vendor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.VendorLoc)
                    .HasColumnName("vendor_loc")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasColumnName("vendor_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VendorPhno)
                    .HasColumnName("vendor_phno")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VendorPwd)
                    .HasColumnName("vendor_pwd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUname)
                    .HasColumnName("vendor_uname")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
