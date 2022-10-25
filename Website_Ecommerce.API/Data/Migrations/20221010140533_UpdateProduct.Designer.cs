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
    [Migration("20221010140533_UpdateProduct")]
    partial class UpdateProduct
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

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
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
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VoucherId")
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

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("VoucherProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

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
                        .HasColumnType("longtext");

                    b.Property<string>("NameMethod")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Material")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Origin")
                        .HasColumnType("longtext");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
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
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shipper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Shippers");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalCategory")
                        .HasColumnType("int");

                    b.Property<int>("TotalRate")
                        .HasColumnType("int");

                    b.Property<int>("UrlAvatar")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("Gender")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UrlAvatar")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

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

                    b.Property<double>("MinPrice")
                        .HasColumnType("double");

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

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<double>("MinPrice")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("VoucherProducts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Cart", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

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
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.VoucherProduct", "VoucherProduct")
                        .WithMany("OrderDetails")
                        .HasForeignKey("VoucherProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

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
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.Shop", "Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Shop");
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

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shipper", b =>
                {
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Order", "Order")
                        .WithOne("Shipper")
                        .HasForeignKey("Website_Ecommerce.API.Data.Entities.Shipper", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Website_Ecommerce.API.Data.Entities.User", "User")
                        .WithOne("Shipper")
                        .HasForeignKey("Website_Ecommerce.API.Data.Entities.Shipper", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

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
                    b.HasOne("Website_Ecommerce.API.Data.Entities.Product", "Product")
                        .WithMany("VoucherProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payment");

                    b.Navigation("Shipper");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Comments");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductDetails");

                    b.Navigation("VoucherProducts");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.ProductDetail", b =>
                {
                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.Shop", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Website_Ecommerce.API.Data.Entities.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Comments");

                    b.Navigation("Orders");

                    b.Navigation("Shipper");

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
