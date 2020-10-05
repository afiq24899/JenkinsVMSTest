using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class IncidentIsFullPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeReceived",
                table: "Incidents");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimePushed",
                table: "Incidents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsFullPath",
                table: "Incidents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimePushed",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "IsFullPath",
                table: "Incidents");

            migrationBuilder.AddColumn<string>(
                name: "DateTimeReceived",
                table: "Incidents",
                type: "text",
                nullable: true);
        }
    }
}
