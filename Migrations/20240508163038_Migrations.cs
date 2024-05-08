using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LocalizacaoModel_Cep",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalizacaoModel",
                table: "LocalizacaoModel");

            migrationBuilder.RenameTable(
                name: "LocalizacaoModel",
                newName: "Localizações");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizações",
                table: "Localizações",
                column: "Cep");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Localizações_Cep",
                table: "Users",
                column: "Cep",
                principalTable: "Localizações",
                principalColumn: "Cep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Localizações_Cep",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizações",
                table: "Localizações");

            migrationBuilder.RenameTable(
                name: "Localizações",
                newName: "LocalizacaoModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalizacaoModel",
                table: "LocalizacaoModel",
                column: "Cep");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LocalizacaoModel_Cep",
                table: "Users",
                column: "Cep",
                principalTable: "LocalizacaoModel",
                principalColumn: "Cep");
        }
    }
}
