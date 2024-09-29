using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace aspdotnet_project.Migrations
{
    /// <inheritdoc />
    public partial class dropFullNameInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ab62251-1fce-4f2f-8815-10cce1c38e69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb827116-e0cb-46b4-8f29-f0c9bcff2185");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ab62251-1fce-4f2f-8815-10cce1c38e69", null, "Admin", "ADMIN" },
                    { "bb827116-e0cb-46b4-8f29-f0c9bcff2185", null, "User", "USER" }
                });
        }
    }
}
