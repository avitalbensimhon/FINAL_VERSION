using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSuperMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class createDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "int", nullable: false),
                    StatusOrders = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuppliersList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliersList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerItem = table.Column<float>(type: "real", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    suppliersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsList_OrdersList_orderId",
                        column: x => x.orderId,
                        principalTable: "OrdersList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsList_SuppliersList_suppliersId",
                        column: x => x.suppliersId,
                        principalTable: "SuppliersList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_orderId",
                table: "GoodsList",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsList_suppliersId",
                table: "GoodsList",
                column: "suppliersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsList");

            migrationBuilder.DropTable(
                name: "OrdersList");

            migrationBuilder.DropTable(
                name: "SuppliersList");
        }
    }
}
