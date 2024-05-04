using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class entityTaskRelation7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_Idlogin",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Idlogin",
                table: "Tasks",
                newName: "IdLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Idlogin",
                table: "Tasks",
                newName: "IX_Tasks_IdLogin");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdLogin",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_IdLogin",
                table: "Tasks",
                column: "IdLogin",
                principalTable: "Login",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Login_IdLogin",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IdLogin",
                table: "Tasks",
                newName: "Idlogin");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdLogin",
                table: "Tasks",
                newName: "IX_Tasks_Idlogin");

            migrationBuilder.AlterColumn<Guid>(
                name: "Idlogin",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Login_Idlogin",
                table: "Tasks",
                column: "Idlogin",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
