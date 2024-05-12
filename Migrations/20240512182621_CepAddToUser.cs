using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class CepAddToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c23e3cf-7344-4f49-a532-498b7c94f287");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b95e41fd-d363-4c0b-87bc-f73cc49a67cd");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e29cfd44-7244-4aaa-83e0-be4a9e45f21c", null, "Admin", "ADMIN" },
                    { "f5668d3f-d968-4328-b51a-9dc650a5aeeb", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e29cfd44-7244-4aaa-83e0-be4a9e45f21c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5668d3f-d968-4328-b51a-9dc650a5aeeb");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c23e3cf-7344-4f49-a532-498b7c94f287", null, "User", "USER" },
                    { "b95e41fd-d363-4c0b-87bc-f73cc49a67cd", null, "Admin", "ADMIN" }
                });
        }
    }
}
