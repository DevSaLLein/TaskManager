using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class refactorDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Usuarios_IdLogin",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IdLogin",
                table: "Tasks",
                newName: "IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdLogin",
                table: "Tasks",
                newName: "IX_Tasks_IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Usuarios_IdUser",
                table: "Tasks",
                column: "IdUser",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Usuarios_IdUser",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Tasks",
                newName: "IdLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdUser",
                table: "Tasks",
                newName: "IX_Tasks_IdLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Usuarios_IdLogin",
                table: "Tasks",
                column: "IdLogin",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
