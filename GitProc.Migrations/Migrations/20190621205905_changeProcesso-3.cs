using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class changeProcesso3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessoId",
                table: "ProcessoMaster",
                newName: "NumeroProcesso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroProcesso",
                table: "ProcessoMaster",
                newName: "ProcessoId");
        }
    }
}
