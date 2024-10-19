using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a43c618-1209-4717-a410-c0b1f8ebb381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5902ff8c-7929-4cac-86be-dd0604be3904");

            migrationBuilder.RenameColumn(
                name: "email_change_token",
                table: "AspNetUsers",
                newName: "change_token");

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
                    { "03068870-e671-49c6-977c-6666236656d5", null, "User", "USER" },
                    { "608c5766-4b49-49eb-a4ea-c2bd878fb13d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03068870-e671-49c6-977c-6666236656d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "608c5766-4b49-49eb-a4ea-c2bd878fb13d");

            migrationBuilder.DropColumn(
                name: "status",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "change_token",
                table: "AspNetUsers",
                newName: "email_change_token");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a43c618-1209-4717-a410-c0b1f8ebb381", null, "Admin", "ADMIN" },
                    { "5902ff8c-7929-4cac-86be-dd0604be3904", null, "User", "USER" }
                });
        }
    }
}
