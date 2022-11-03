using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website_Ecommerce.API.Data.Migrations
{
    public partial class fixCartProductDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Carts",
                newName: "ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                newName: "IX_Carts_ProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductDetails_ProductDetailId",
                table: "Carts",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductDetails_ProductDetailId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "Carts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductDetailId",
                table: "Carts",
                newName: "IX_Carts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
