using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class tempcolAlibabaAccident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlibabaAccidentImage",
                table: "Displays",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AlibabaCreationDate",
                table: "Displays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AlibabaText",
                table: "Displays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlibabaAccidentImage",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "AlibabaCreationDate",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "AlibabaText",
                table: "Displays");
        }
    }
}
