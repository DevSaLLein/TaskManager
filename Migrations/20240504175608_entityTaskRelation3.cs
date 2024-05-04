using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class entityTaskRelation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_Id",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LoginId",
                table: "Tasks",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_LoginId",
                table: "Tasks",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_LoginId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_LoginId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_Id",
                table: "Tasks",
                column: "Id",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
