using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class alterandoProcessoEstrutura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdvogadoId",
                table: "Processos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comarca",
                table: "Processos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAdicionado",
                table: "Processos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Processos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processos_AdvogadoId",
                table: "Processos",
                column: "AdvogadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processos_Advogados_AdvogadoId",
                table: "Processos",
                column: "AdvogadoId",
                principalTable: "Advogados",
                principalColumn: "AdvogadoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processos_Advogados_AdvogadoId",
                table: "Processos");

            migrationBuilder.DropIndex(
                name: "IX_Processos_AdvogadoId",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "AdvogadoId",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "Comarca",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "DataAdicionado",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Processos");
        }
    }
}
