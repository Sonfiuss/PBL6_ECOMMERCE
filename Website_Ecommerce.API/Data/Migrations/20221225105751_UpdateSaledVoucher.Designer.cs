﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Website_Ecommerce.API.Data;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221225105751_UpdateSaledVoucher")]
    partial class UpdateSaledVoucher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ProductDetailId")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("CustomRecipientsDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RecipientPhone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("VoucherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("ProductDetailId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<DateTime?>("ShopConfirmDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShopSendDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("VoucherProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductDetailId");

                    b.HasIndex("ProductDetailId");

                    b.HasIndex("VoucherProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Config")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NameMethod")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AverageRate")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TotalRate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Booked")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<double>("InitialPrice")
                        .HasColumnType("double");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Saled")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductDetailId")
                        .HasColumnType("int");

                    b.Property<string>("UrlImage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<float>("AverageRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TotalCategory")
                        .HasColumnType("int");

                    b.Property<int>("TotalProduct")
                        .HasColumnType("int");

                    b.Property<int>("TotalRate")
                        .HasColumnType("int");

                    b.Property<string>("UrlAvatar")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("Gender")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsBlock")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("UrlAvatar")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.VoucherOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Booked")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("MinPrice")
                        .HasColumnType("double");

                    b.Property<int>("Sale")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("VoucherOrders");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.VoucherProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Booked")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("MinPrice")
                        .HasColumnType("double");

                    b.Property<int>("Sale")
                        .HasColumnType("int");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("VoucherProducts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Cart", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.ProductDetail", "ProductDetail")
                        .WithMany("Carts")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDetail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Comment", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Order", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.VoucherOrder", "VoucherOrder")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId");

                    b.Navigation("User");

                    b.Navigation("VoucherOrder");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.ProductDetail", "ProductDetail")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.VoucherProduct", "VoucherProduct")
                        .WithMany("OrderDetails")
                        .HasForeignKey("VoucherProductId");

                    b.Navigation("Order");

                    b.Navigation("ProductDetail");

                    b.Navigation("VoucherProduct");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Payment", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("Website_Ecommerce.API.Data.Entities.Payment", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Product", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Shop", "Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductCategory", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductDetail", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("ProductDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductImage", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.ProductDetail", "ProductDetail")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shop", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithOne("Shop")
                        .HasForeignKey("Website_Ecommerce.API.Data.Entities.Shop", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.UserRole", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.VoucherProduct", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Shop", "Shop")
                        .WithMany("VoucherProducts")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProductCategories");

                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductDetail", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shop", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("VoucherProducts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Comments");

                    b.Navigation("Orders");

                    b.Navigation("Shop");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.VoucherOrder", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.VoucherProduct", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
