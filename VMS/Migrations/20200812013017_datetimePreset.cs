using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class datetimePreset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "GroupPresets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "GroupPresets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "GroupPresets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "GroupPresets",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
