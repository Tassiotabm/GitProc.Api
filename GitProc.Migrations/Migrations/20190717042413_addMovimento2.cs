using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitProc.Migrations.Migrations
{
    public partial class addMovimento2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessMasterId",
                table: "Movimentos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessMasterId",
                table: "Movimentos");
        }
    }
}
