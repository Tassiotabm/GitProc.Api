using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class allowNullEscritorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advogados_Escritorios_EscritorioId",
                table: "Advogados");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscritorioId",
                table: "Advogados",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Advogados_Escritorios_EscritorioId",
                table: "Advogados",
                column: "EscritorioId",
                principalTable: "Escritorios",
                principalColumn: "EscritorioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advogados_Escritorios_EscritorioId",
                table: "Advogados");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscritorioId",
                table: "Advogados",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advogados_Escritorios_EscritorioId",
                table: "Advogados",
                column: "EscritorioId",
                principalTable: "Escritorios",
                principalColumn: "EscritorioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
