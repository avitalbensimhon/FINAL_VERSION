using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSuperMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class lastlastlastchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idsuppliers",
                table: "GoodsList");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idsuppliers",
                table: "GoodsList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
