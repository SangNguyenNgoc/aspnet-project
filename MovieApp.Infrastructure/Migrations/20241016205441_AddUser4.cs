using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUser4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cc08336-04b3-42f1-ae53-27415769363c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caf24d3d-babb-462d-bd88-0a08d0d480a3");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29955e0e-543e-4580-bb60-7adf929a3d6f", null, "Admin", "ADMIN" },
                    { "b062a191-5309-4377-af8f-5cb13949a19e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29955e0e-543e-4580-bb60-7adf929a3d6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b062a191-5309-4377-af8f-5cb13949a19e");

            migrationBuilder.DropColumn(
                name: "status",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cc08336-04b3-42f1-ae53-27415769363c", null, "Admin", "ADMIN" },
                    { "caf24d3d-babb-462d-bd88-0a08d0d480a3", null, "User", "USER" }
                });
        }
    }
}
