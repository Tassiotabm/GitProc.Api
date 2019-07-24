using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addMovimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_ProcessoMaster_ProcessoMasterId",
                table: "Movimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "ProcessoNoTribunal",
                table: "Movimento");

            migrationBuilder.RenameTable(
                name: "Movimento",
                newName: "Movimentos");

            migrationBuilder.RenameColumn(
                name: "TipoMovimento",
                table: "Movimentos",
                newName: "MovimentoTitulo");

            migrationBuilder.RenameIndex(
                name: "IX_Movimento_ProcessoMasterId",
                table: "Movimentos",
                newName: "IX_Movimentos_ProcessoMasterId");

            migrationBuilder.AddColumn<List<string>>(
                name: "MovimentoData",
                table: "Movimentos",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "MovimentoTag",
                table: "Movimentos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movimentos",
                table: "Movimentos",
                column: "MovimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessoMasterId",
                table: "Movimentos",
                column: "ProcessoMasterId",
                principalTable: "ProcessoMaster",
                principalColumn: "ProcessoMasterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessoMasterId",
                table: "Movimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movimentos",
                table: "Movimentos");

            migrationBuilder.DropColumn(
                name: "MovimentoData",
                table: "Movimentos");

            migrationBuilder.DropColumn(
                name: "MovimentoTag",
                table: "Movimentos");

            migrationBuilder.RenameTable(
                name: "Movimentos",
                newName: "Movimento");

            migrationBuilder.RenameColumn(
                name: "MovimentoTitulo",
                table: "Movimento",
                newName: "TipoMovimento");

            migrationBuilder.RenameIndex(
                name: "IX_Movimentos_ProcessoMasterId",
                table: "Movimento",
                newName: "IX_Movimento_ProcessoMasterId");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Movimento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Movimento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessoNoTribunal",
                table: "Movimento",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento",
                column: "MovimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_ProcessoMaster_ProcessoMasterId",
                table: "Movimento",
                column: "ProcessoMasterId",
                principalTable: "ProcessoMaster",
                principalColumn: "ProcessoMasterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
