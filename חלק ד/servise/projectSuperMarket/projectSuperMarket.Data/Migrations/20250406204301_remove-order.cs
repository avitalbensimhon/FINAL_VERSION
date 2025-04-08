using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSuperMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_OrdersList_orderId",
                table: "GoodsList");

            migrationBuilder.DropIndex(
                name: "IX_GoodsList_orderId",
                table: "GoodsList");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "GoodsList");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "GoodsList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_OrdersId",
                table: "GoodsList",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_OrdersList_OrdersId",
                table: "GoodsList",
                column: "OrdersId",
                principalTable: "OrdersList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsList_OrdersList_OrdersId",
                table: "GoodsList");

            migrationBuilder.DropIndex(
                name: "IX_GoodsList_OrdersId",
                table: "GoodsList");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "GoodsList");

            migrationBuilder.AddColumn<int>(
                name: "orderId",
                table: "GoodsList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_orderId",
                table: "GoodsList",
                column: "orderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsList_OrdersList_orderId",
                table: "GoodsList",
                column: "orderId",
                principalTable: "OrdersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
