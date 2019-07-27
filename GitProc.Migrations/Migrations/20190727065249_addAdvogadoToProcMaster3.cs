using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addAdvogadoToProcMaster3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processos_Advogados_AdvogadoId",
                table: "Processos");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvogadoId",
                table: "Processos",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "AdvogadoId",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdvogadoId",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoMaster_AdvogadoId",
                table: "ProcessoMaster",
                column: "AdvogadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_AdvogadoId",
                table: "Comentarios",
                column: "AdvogadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Advogados_AdvogadoId",
                table: "Comentarios",
                column: "AdvogadoId",
                principalTable: "Advogados",
                principalColumn: "AdvogadoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessoMaster_Advogados_AdvogadoId",
                table: "ProcessoMaster",
                column: "AdvogadoId",
                principalTable: "Advogados",
                principalColumn: "AdvogadoId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Comentarios_Advogados_AdvogadoId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessoMaster_Advogados_AdvogadoId",
                table: "ProcessoMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Processos_Advogados_AdvogadoId",
                table: "Processos");

            migrationBuilder.DropIndex(
                name: "IX_ProcessoMaster_AdvogadoId",
                table: "ProcessoMaster");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_AdvogadoId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "AdvogadoId",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "AdvogadoId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvogadoId",
                table: "Processos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processos_Advogados_AdvogadoId",
                table: "Processos",
                column: "AdvogadoId",
                principalTable: "Advogados",
                principalColumn: "AdvogadoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
