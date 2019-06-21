using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class changeProcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessoMasterId",
                table: "Processos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Acao",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Advogados",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assunto",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classe",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comarca",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDistribuicao",
                table: "ProcessoMaster",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVerificacao",
                table: "ProcessoMaster",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instancia",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessoId",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tribunal",
                table: "ProcessoMaster",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDay",
                table: "ProcessoMaster",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Movimento",
                columns: table => new
                {
                    MovimentoId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    ProcessoNoTribunal = table.Column<string>(nullable: true),
                    TipoMovimento = table.Column<string>(nullable: true),
                    Localizacao = table.Column<string>(nullable: true),
                    ProcessoMasterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimento", x => x.MovimentoId);
                    table.ForeignKey(
                        name: "FK_Movimento_ProcessoMaster_ProcessoMasterId",
                        column: x => x.ProcessoMasterId,
                        principalTable: "ProcessoMaster",
                        principalColumn: "ProcessoMasterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processos_ProcessoMasterId",
                table: "Processos",
                column: "ProcessoMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_ProcessoMasterId",
                table: "Movimento",
                column: "ProcessoMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processos_ProcessoMaster_ProcessoMasterId",
                table: "Processos",
                column: "ProcessoMasterId",
                principalTable: "ProcessoMaster",
                principalColumn: "ProcessoMasterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processos_ProcessoMaster_ProcessoMasterId",
                table: "Processos");

            migrationBuilder.DropTable(
                name: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Processos_ProcessoMasterId",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "ProcessoMasterId",
                table: "Processos");

            migrationBuilder.DropColumn(
                name: "Acao",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Advogados",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Assunto",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Classe",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Comarca",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "DataDistribuicao",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "DataVerificacao",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Instancia",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "ProcessoId",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "Tribunal",
                table: "ProcessoMaster");

            migrationBuilder.DropColumn(
                name: "UpdatedDay",
                table: "ProcessoMaster");
        }
    }
}
