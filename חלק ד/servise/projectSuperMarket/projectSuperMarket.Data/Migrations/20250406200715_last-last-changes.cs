using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSuperMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class lastlastchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_SuppliersList_SuppliersId",
                table: "GoodsList");

            migrationBuilder.DropIndex(
                name: "IX_GoodsList_SuppliersId",
                table: "GoodsList");

            migrationBuilder.DropColumn(
                name: "SuppliersId",
                table: "GoodsList");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "GoodsList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_SupplierId",
                table: "GoodsList",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_SuppliersList_SupplierId",
                table: "GoodsList",
                column: "SupplierId",
                principalTable: "SuppliersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_SuppliersList_SupplierId",
                table: "GoodsList");

            migrationBuilder.DropIndex(
                name: "IX_GoodsList_SupplierId",
                table: "GoodsList");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "GoodsList");

            migrationBuilder.AddColumn<int>(
                name: "SuppliersId",
                table: "GoodsList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_SuppliersId",
                table: "GoodsList",
                column: "SuppliersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_SuppliersList_SuppliersId",
                table: "GoodsList",
                column: "SuppliersId",
                principalTable: "SuppliersList",
                principalColumn: "Id");
        }
    }
}
