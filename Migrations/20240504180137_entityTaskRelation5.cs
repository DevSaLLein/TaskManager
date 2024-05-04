using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class entityTaskRelation5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_LoginId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "LoginId",
                table: "Tasks",
                newName: "Idlogin");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_LoginId",
                table: "Tasks",
                newName: "IX_Tasks_Idlogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_Idlogin",
                table: "Tasks",
                column: "Idlogin",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_Idlogin",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Idlogin",
                table: "Tasks",
                newName: "LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Idlogin",
                table: "Tasks",
                newName: "IX_Tasks_LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_LoginId",
                table: "Tasks",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
