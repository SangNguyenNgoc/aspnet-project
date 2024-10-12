using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace aspdotnet_project.Migrations
{
    /// <inheritdoc />
    public partial class initAddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "572c3f6e-22e0-471f-a3f8-ad4acdff7125");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1503442-f971-4559-8ef4-f323fcec6b4f");

            migrationBuilder.AddColumn<long>(
                name: "location_id",
                table: "cinemas",
                type: "bigint",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "572c3f6e-22e0-471f-a3f8-ad4acdff7125", null, "User", "USER" },
                    { "f1503442-f971-4559-8ef4-f323fcec6b4f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cinemas_location_id",
                table: "cinemas",
                column: "location_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cinemas_locations_location_id",
                table: "cinemas",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinemas_locations_location_id",
                table: "cinemas");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropIndex(
                name: "IX_cinemas_location_id",
                table: "cinemas");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "572c3f6e-22e0-471f-a3f8-ad4acdff7125");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1503442-f971-4559-8ef4-f323fcec6b4f");

            migrationBuilder.DropColumn(
                name: "location_id",
                table: "cinemas");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61f1232f-9c6c-457a-95bd-ad653e732974", null, "User", "USER" },
                    { "a6bd6a93-9011-45e9-a89f-06c7c2f455f5", null, "Admin", "ADMIN" }
                });
        }
    }
}
