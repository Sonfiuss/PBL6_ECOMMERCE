using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
