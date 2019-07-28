using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addAdvogadoToProcMaster5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comarca",
                table: "Processos");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Processos",
                newName: "Nick");

            migrationBuilder.AddColumn<string>(
                name: "Nick",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileFormat",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Comentarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nick",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "FileFormat",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "Nick",
                table: "Processos",
                newName: "Numero");

            migrationBuilder.AddColumn<string>(
                name: "Comarca",
                table: "Processos",
                nullable: true);
        }
    }
}
