using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class entityTaskRelation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Tasks_Id",
                table: "Login");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_Id",
                table: "Tasks",
                column: "Id",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_Id",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Tasks_Id",
                table: "Login",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
