using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addComentario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComentarioData",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriado",
                table: "Comentarios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessoId",
                table: "Comentarios",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ProcessoId",
                table: "Comentarios",
                column: "ProcessoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Processos_ProcessoId",
                table: "Comentarios",
                column: "ProcessoId",
                principalTable: "Processos",
                principalColumn: "ProcessoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Processos_ProcessoId",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_ProcessoId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ComentarioData",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "DataCriado",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ProcessoId",
                table: "Comentarios");
        }
    }
}
