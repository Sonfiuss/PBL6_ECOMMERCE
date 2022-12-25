using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class OrderRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Orders");
        }
    }
}
