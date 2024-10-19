using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initShowsAndTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78258f89-0844-4310-80e2-2f97c60f8258");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2ad0ed4-10ed-404e-a38e-491073d93fe2");

            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    running_time = table.Column<int>(type: "int(11)", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    status = table.Column<ulong>(type: "bit(1)", nullable: false),
                    movie_id = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    format_id = table.Column<long>(type: "bigint", nullable: false),
                    hall_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shows", x => x.id);
                    table.ForeignKey(
                        name: "FK_shows_formats_format_id",
                        column: x => x.format_id,
                        principalTable: "formats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shows_halls_hall_id",
                        column: x => x.hall_id,
                        principalTable: "halls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shows_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bill_id = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    show_id = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seat_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_bills_bill_id",
                        column: x => x.bill_id,
                        principalTable: "bills",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tickets_seats_seat_id",
                        column: x => x.seat_id,
                        principalTable: "seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_shows_show_id",
                        column: x => x.show_id,
                        principalTable: "shows",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ab62251-1fce-4f2f-8815-10cce1c38e69", null, "Admin", "ADMIN" },
                    { "bb827116-e0cb-46b4-8f29-f0c9bcff2185", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_shows_format_id",
                table: "shows",
                column: "format_id");

            migrationBuilder.CreateIndex(
                name: "IX_shows_hall_id",
                table: "shows",
                column: "hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_shows_movie_id",
                table: "shows",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_bill_id",
                table: "tickets",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_seat_id",
                table: "tickets",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_show_id",
                table: "tickets",
                column: "show_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "shows");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ab62251-1fce-4f2f-8815-10cce1c38e69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb827116-e0cb-46b4-8f29-f0c9bcff2185");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78258f89-0844-4310-80e2-2f97c60f8258", null, "Admin", "ADMIN" },
                    { "f2ad0ed4-10ed-404e-a38e-491073d93fe2", null, "User", "USER" }
                });
        }
    }
}
