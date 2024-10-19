using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initCinemasHallsSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16efe634-1568-4fe0-ae71-e561069454b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ff1981e-8402-4c3a-8b59-061717ae960d");

            migrationBuilder.CreateTable(
                name: "cinema_status",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinema_status", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "hall_status",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hall_status", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cinemas",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hotline = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinemas", x => x.id);
                    table.ForeignKey(
                        name: "FK_cinemas_cinema_status_status_id",
                        column: x => x.status_id,
                        principalTable: "cinema_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "halls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    number_of_rows = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seats_per_row = table.Column<int>(type: "int(11)", nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
                    cinema_id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_halls", x => x.id);
                    table.ForeignKey(
                        name: "FK_halls_cinemas_cinema_id",
                        column: x => x.cinema_id,
                        principalTable: "cinemas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_halls_hall_status_status_id",
                        column: x => x.status_id,
                        principalTable: "hall_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    row_index = table.Column<int>(type: "int(11)", nullable: false),
                    row_name = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int(11)", nullable: false),
                    Status = table.Column<ulong>(type: "bit(1)", nullable: false),
                    hall_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.id);
                    table.ForeignKey(
                        name: "FK_seats_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cdba987a-5b1b-46bd-bb5f-4cf6a564d319", null, "User", "USER" },
                    { "e3a493b6-b3ef-4a9e-98e5-bfc6f96db4b6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cinemas_status_id",
                table: "cinemas",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_halls_cinema_id",
                table: "halls",
                column: "cinema_id");

            migrationBuilder.CreateIndex(
                name: "IX_halls_status_id",
                table: "halls",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_seats_hall_id",
                table: "seats",
                column: "hall_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "halls");

            migrationBuilder.DropTable(
                name: "cinemas");

            migrationBuilder.DropTable(
                name: "hall_status");

            migrationBuilder.DropTable(
                name: "cinema_status");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdba987a-5b1b-46bd-bb5f-4cf6a564d319");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3a493b6-b3ef-4a9e-98e5-bfc6f96db4b6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16efe634-1568-4fe0-ae71-e561069454b3", null, "Admin", "ADMIN" },
                    { "8ff1981e-8402-4c3a-8b59-061717ae960d", null, "User", "USER" }
                });
        }
    }
}
