using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class entityTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdLogin",
                table: "Tasks",
                newName: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Tasks_Id",
                table: "Login",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Tasks_Id",
                table: "Login");

            migrationBuilder.RenameColumn(
                name: "LoginId",
                table: "Tasks",
                newName: "IdLogin");
        }
    }
}
