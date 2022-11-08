using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Migrations
{
    public partial class UpdateVoucherProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherProducts_Products_ProductId",
                table: "VoucherProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "VoucherProducts",
                newName: "ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherProducts_ProductId",
                table: "VoucherProducts",
                newName: "IX_VoucherProducts_ShopId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Shops",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherProducts_Shops_ShopId",
                table: "VoucherProducts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherProducts_Shops_ShopId",
                table: "VoucherProducts");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "VoucherProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherProducts_ShopId",
                table: "VoucherProducts",
                newName: "IX_VoucherProducts_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Shops",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherProducts_Products_ProductId",
                table: "VoucherProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
