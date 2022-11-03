using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class fixDBShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlAvatar",
                table: "Shops",
                type: "varchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Shops",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UrlAvatar",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 512,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Shops",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
