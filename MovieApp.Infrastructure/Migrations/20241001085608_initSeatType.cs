using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initSeatType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32eb78fa-8372-498c-8af0-7bad8e9bbf53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a305829-13f9-41ec-b19c-b0969aaf9bb9");

            migrationBuilder.AddColumn<long>(
                name: "type_id",
                table: "seats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "seat_type",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat_type", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c7020d0-aed5-49f2-ac8b-04e9629b127e", null, "Admin", "ADMIN" },
                    { "bf044299-d79a-42e2-a44f-a9b1c3910f71", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_seats_type_id",
                table: "seats",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_seats_seat_type_type_id",
                table: "seats",
                column: "type_id",
                principalTable: "seat_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_seats_seat_type_type_id",
                table: "seats");

            migrationBuilder.DropTable(
                name: "seat_type");

            migrationBuilder.DropIndex(
                name: "IX_seats_type_id",
                table: "seats");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c7020d0-aed5-49f2-ac8b-04e9629b127e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf044299-d79a-42e2-a44f-a9b1c3910f71");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "seats");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32eb78fa-8372-498c-8af0-7bad8e9bbf53", null, "Admin", "ADMIN" },
                    { "8a305829-13f9-41ec-b19c-b0969aaf9bb9", null, "User", "USER" }
                });
        }
    }
}
