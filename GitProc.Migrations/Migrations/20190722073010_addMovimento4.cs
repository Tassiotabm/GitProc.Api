using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addMovimento4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessoMasterId",
                table: "Movimentos");

            migrationBuilder.DropIndex(
                name: "IX_Movimentos_ProcessoMasterId",
                table: "Movimentos");

            migrationBuilder.DropColumn(
                name: "ProcessoMasterId",
                table: "Movimentos");

            migrationBuilder.AlterColumn<string>(
                name: "MovimentoTag",
                table: "Movimentos",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MovimentoData",
                table: "Movimentos",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_ProcessMasterId",
                table: "Movimentos",
                column: "ProcessMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessMasterId",
                table: "Movimentos",
                column: "ProcessMasterId",
                principalTable: "ProcessoMaster",
                principalColumn: "ProcessoMasterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessMasterId",
                table: "Movimentos");

            migrationBuilder.DropIndex(
                name: "IX_Movimentos_ProcessMasterId",
                table: "Movimentos");

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessoMasterId",
                table: "Movimentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_ProcessoMasterId",
                table: "Movimentos",
                column: "ProcessoMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentos_ProcessoMaster_ProcessoMasterId",
                table: "Movimentos",
                column: "ProcessoMasterId",
                principalTable: "ProcessoMaster",
                principalColumn: "ProcessoMasterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
