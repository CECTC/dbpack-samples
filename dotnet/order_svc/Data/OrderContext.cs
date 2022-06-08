using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using order_svc.Models;

namespace order_svc.Data
{
    public partial class OrderContext : DbContext
    {
        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SoItem> SoItems { get; set; } = null!;
        public virtual DbSet<SoMaster> SoMasters { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<SoItem>(entity =>
            {
                entity.HasKey(e => e.Sysno)
                    .HasName("PRIMARY");

                entity.ToTable("so_item");

                entity.HasComment("订单明细表");

                entity.Property(e => e.Sysno).HasColumnName("sysno");

                entity.Property(e => e.CostPrice)
                    .HasPrecision(16, 6)
                    .HasColumnName("cost_price")
                    .HasComment("成本价");

                entity.Property(e => e.DealPrice)
                    .HasPrecision(16, 6)
                    .HasColumnName("deal_price")
                    .HasComment("成交价");

                entity.Property(e => e.OriginalPrice)
                    .HasPrecision(16, 6)
                    .HasColumnName("original_price")
                    .HasComment("原价");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(64)
                    .HasColumnName("product_name")
                    .HasComment("商品名称");

                entity.Property(e => e.ProductSysno).HasColumnName("product_sysno");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasComment("数量");

                entity.Property(e => e.SoSysno).HasColumnName("so_sysno");
            });

            modelBuilder.Entity<SoMaster>(entity =>
            {
                entity.HasKey(e => e.Sysno)
                    .HasName("PRIMARY");

                entity.ToTable("so_master");

                entity.HasComment("订单表");

                entity.Property(e => e.Sysno)
                    .ValueGeneratedNever()
                    .HasColumnName("sysno");

                entity.Property(e => e.Appid)
                    .HasMaxLength(64)
                    .HasColumnName("appid")
                    .HasComment("订单来源");

                entity.Property(e => e.BuyerUserSysno)
                    .HasColumnName("buyer_user_sysno")
                    .HasComment("下单用户号");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("create_user");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("delivery_date")
                    .HasComment("发货时间");

                entity.Property(e => e.GmtCreate)
                    .HasColumnType("timestamp")
                    .HasColumnName("gmt_create");

                entity.Property(e => e.GmtModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("gmt_modified");

                entity.Property(e => e.Memo)
                    .HasMaxLength(255)
                    .HasColumnName("memo")
                    .HasComment("备注");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(255)
                    .HasColumnName("modify_user");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("order_date")
                    .HasComment("下单时间");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("payment_date")
                    .HasComment("支付时间");

                entity.Property(e => e.PaymentType)
                    .HasColumnName("payment_type")
                    .HasComment("支付方式：1，支付宝，2，微信");

                entity.Property(e => e.ReceiveAddress)
                    .HasMaxLength(200)
                    .HasColumnName("receive_address");

                entity.Property(e => e.ReceiveContact)
                    .HasMaxLength(20)
                    .HasColumnName("receive_contact");

                entity.Property(e => e.ReceiveContactPhone)
                    .HasMaxLength(100)
                    .HasColumnName("receive_contact_phone");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("receive_date")
                    .HasComment("发货时间");

                entity.Property(e => e.ReceiveDivisionSysno).HasColumnName("receive_division_sysno");

                entity.Property(e => e.ReceiveZip)
                    .HasMaxLength(20)
                    .HasColumnName("receive_zip");

                entity.Property(e => e.SellerCompanyCode)
                    .HasMaxLength(20)
                    .HasColumnName("seller_company_code")
                    .HasComment("卖家公司编号");

                entity.Property(e => e.SoAmt)
                    .HasPrecision(16, 6)
                    .HasColumnName("so_amt")
                    .HasComment("订单总额");

                entity.Property(e => e.SoId)
                    .HasMaxLength(20)
                    .HasColumnName("so_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasComment("10,创建成功，待支付；30；支付成功，待发货；50；发货成功，待收货；70，确认收货，已完成；90，下单失败；100已作废");

                entity.Property(e => e.StockSysno).HasColumnName("stock_sysno");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
