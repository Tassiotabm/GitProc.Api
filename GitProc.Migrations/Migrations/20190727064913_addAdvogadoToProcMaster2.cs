using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addAdvogadoToProcMaster2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Arquivos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdvogadoId",
                table: "ProcessoMaster",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdvogadoId",
                table: "Comentarios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    ArquivosId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.ArquivosId);
                });

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
