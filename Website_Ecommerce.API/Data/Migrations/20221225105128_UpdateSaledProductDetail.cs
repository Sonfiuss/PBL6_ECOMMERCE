using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class UpdateSaledProductDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saled",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Booked",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Saled",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "Saled",
                table: "ProductDetails");

            migrationBuilder.AddColumn<int>(
                name: "Saled",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
