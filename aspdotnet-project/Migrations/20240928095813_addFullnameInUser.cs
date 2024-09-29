using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace aspdotnet_project.Migrations
{
    /// <inheritdoc />
    public partial class addFullnameInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35f9d6b7-172c-4c77-b554-3c2e0f6a1d1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6779ff5-736e-4f96-8f42-9be57507ea5b");

            migrationBuilder.AddColumn<string>(
                name: "fullname",
                table: "AspNetUsers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32eb78fa-8372-498c-8af0-7bad8e9bbf53", null, "Admin", "ADMIN" },
                    { "8a305829-13f9-41ec-b19c-b0969aaf9bb9", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32eb78fa-8372-498c-8af0-7bad8e9bbf53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a305829-13f9-41ec-b19c-b0969aaf9bb9");

            migrationBuilder.DropColumn(
                name: "fullname",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35f9d6b7-172c-4c77-b554-3c2e0f6a1d1b", null, "User", "USER" },
                    { "a6779ff5-736e-4f96-8f42-9be57507ea5b", null, "Admin", "ADMIN" }
                });
        }
    }
}
