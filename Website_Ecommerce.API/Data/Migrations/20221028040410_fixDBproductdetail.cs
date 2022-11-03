using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class fixDBproductdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitialPrice",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialPrice",
                table: "ProductDetails");
        }
    }
}
