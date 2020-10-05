using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class UpdateUptimeReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyUptime",
                table: "UptimeReports");

            migrationBuilder.DropColumn(
                name: "DownTime",
                table: "UptimeReports");

            migrationBuilder.DropColumn(
                name: "UpTime",
                table: "UptimeReports");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "UptimeReports",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "TimeEnd",
                table: "UptimeReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeStart",
                table: "UptimeReports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "UptimeReports");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "UptimeReports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UptimeReports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyUptime",
                table: "UptimeReports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DownTime",
                table: "UptimeReports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpTime",
                table: "UptimeReports",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
