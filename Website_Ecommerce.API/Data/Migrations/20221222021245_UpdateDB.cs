using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VoucherProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VoucherOrders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Shops",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalProduct",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "AverageRate",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Saled",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRate",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Categories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            // migrationBuilder.Sql("INSERT INTO PBL6_ECOMMERCE.Categories (Name, CreateBy, DateCreate) VALUES('Test', 1, '0001-01-01 00:00:00.000000') ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VoucherProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VoucherOrders");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "TotalProduct",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Saled",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Categories");
            // migrationBuilder.Sql("DELETE FROM PBL6_ECOMMERCE.Categories WHERE Name = 'Test' ");
        }
    }
}
