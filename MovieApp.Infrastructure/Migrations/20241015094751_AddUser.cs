using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c7020d0-aed5-49f2-ac8b-04e9629b127e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf044299-d79a-42e2-a44f-a9b1c3910f71");

            migrationBuilder.AddColumn<string>(
                name: "email_change_token",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "new_email",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a43c618-1209-4717-a410-c0b1f8ebb381", null, "Admin", "ADMIN" },
                    { "5902ff8c-7929-4cac-86be-dd0604be3904", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a43c618-1209-4717-a410-c0b1f8ebb381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5902ff8c-7929-4cac-86be-dd0604be3904");

            migrationBuilder.DropColumn(
                name: "email_change_token",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "new_email",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c7020d0-aed5-49f2-ac8b-04e9629b127e", null, "Admin", "ADMIN" },
                    { "bf044299-d79a-42e2-a44f-a9b1c3910f71", null, "User", "USER" }
                });
        }
    }
}
