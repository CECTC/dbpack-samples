using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using product_svc.Models;

namespace product_svc.Data
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.Sysno)
                    .HasName("PRIMARY");

                entity.ToTable("inventory");

                entity.HasComment("商品库存");

                entity.Property(e => e.Sysno)
                    .HasColumnName("sysno")
                    .HasComment("主键");

                entity.Property(e => e.AccountQty)
                    .HasColumnName("account_qty")
                    .HasComment("财务库存");

                entity.Property(e => e.AdjustLockedQty)
                    .HasColumnName("adjust_locked_qty")
                    .HasComment("调整锁定库存");

                entity.Property(e => e.AllocatedQty)
                    .HasColumnName("allocated_qty")
                    .HasComment("分配库存");

                entity.Property(e => e.AvailableQty)
                    .HasColumnName("available_qty")
                    .HasComment("可用库存");

                entity.Property(e => e.ProductSysno).HasColumnName("product_sysno");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Sysno)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasComment("商品SKU");

                entity.Property(e => e.Sysno)
                    .HasColumnName("sysno")
                    .HasComment("主键");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(30)
                    .HasColumnName("barcode");

                entity.Property(e => e.C3Sysno).HasColumnName("c3_sysno");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(32)
                    .HasColumnName("create_user")
                    .HasComment("创建人");

                entity.Property(e => e.DefaultImageSrc)
                    .HasMaxLength(200)
                    .HasColumnName("default_image_src");

                entity.Property(e => e.GmtCreate)
                    .HasColumnType("timestamp")
                    .HasColumnName("gmt_create")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GmtModified)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("gmt_modified")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("修改时间");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.MerchantProductid)
                    .HasMaxLength(20)
                    .HasColumnName("merchant_productid");

                entity.Property(e => e.MerchantSysno).HasColumnName("merchant_sysno");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(32)
                    .HasColumnName("modify_user")
                    .HasComment("修改人");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(2000)
                    .HasColumnName("product_desc")
                    .HasComment("描述");

                entity.Property(e => e.ProductDescLong)
                    .HasColumnType("text")
                    .HasColumnName("product_desc_long")
                    .HasComment("描述");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(32)
                    .HasColumnName("product_name")
                    .HasComment("品名");

                entity.Property(e => e.ProductTitle)
                    .HasMaxLength(32)
                    .HasColumnName("product_title");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1，待上架；2，上架；3，下架；4，售罄下架；5，违规下架");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
