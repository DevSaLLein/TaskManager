using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class EditNameEntityTasksItensToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksItens_UsersSign_IdUser",
                table: "TasksItens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksItens",
                table: "TasksItens");

            migrationBuilder.RenameTable(
                name: "TasksItens",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_TasksItens_IdUser",
                table: "Tasks",
                newName: "IX_Tasks_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UsersSign_IdUser",
                table: "Tasks",
                column: "IdUser",
                principalTable: "UsersSign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UsersSign_IdUser",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TasksItens");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdUser",
                table: "TasksItens",
                newName: "IX_TasksItens_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksItens",
                table: "TasksItens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksItens_UsersSign_IdUser",
                table: "TasksItens",
                column: "IdUser",
                principalTable: "UsersSign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
