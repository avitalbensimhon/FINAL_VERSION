using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSuperMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class changesuppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_SuppliersList_suppliersId",
                table: "GoodsList");

            migrationBuilder.RenameColumn(
                name: "suppliersId",
                table: "GoodsList",
                newName: "SuppliersId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsList_suppliersId",
                table: "GoodsList",
                newName: "IX_GoodsList_SuppliersId");

            migrationBuilder.AlterColumn<int>(
                name: "SuppliersId",
                table: "GoodsList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Idsuppliers",
                table: "GoodsList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_SuppliersList_SuppliersId",
                table: "GoodsList",
                column: "SuppliersId",
                principalTable: "SuppliersList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_SuppliersList_SuppliersId",
                table: "GoodsList");

            migrationBuilder.DropColumn(
                name: "Idsuppliers",
                table: "GoodsList");

            migrationBuilder.RenameColumn(
                name: "SuppliersId",
                table: "GoodsList",
                newName: "suppliersId");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsList_SuppliersId",
                table: "GoodsList",
                newName: "IX_GoodsList_suppliersId");

            migrationBuilder.AlterColumn<int>(
                name: "suppliersId",
                table: "GoodsList",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_SuppliersList_suppliersId",
                table: "GoodsList",
                column: "suppliersId",
                principalTable: "SuppliersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
