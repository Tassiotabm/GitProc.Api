using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class changeProcesso1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessoVersionados");

            migrationBuilder.AddColumn<Guid>(
                name: "EscritorioId",
                table: "Processos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processos_EscritorioId",
                table: "Processos",
                column: "EscritorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processos_Escritorios_EscritorioId",
                table: "Processos",
                column: "EscritorioId",
                principalTable: "Escritorios",
                principalColumn: "EscritorioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processos_Escritorios_EscritorioId",
                table: "Processos");

            migrationBuilder.DropIndex(
                name: "IX_Processos_EscritorioId",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "EscritorioId",
                table: "Processos");

            migrationBuilder.CreateTable(
                name: "ProcessoVersionados",
                columns: table => new
                {
                    ProcessoVersionadoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoVersionados", x => x.ProcessoVersionadoId);
                });
        }
    }
}
