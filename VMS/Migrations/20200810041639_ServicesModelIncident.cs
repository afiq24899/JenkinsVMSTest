using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class ServicesModelIncident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateTimeReceived",
                table: "Incidents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeReceived",
                table: "Incidents",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
