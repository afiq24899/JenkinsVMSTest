using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class removedummypreset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "DateTimeReceived",
                table: "Incidents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeReceived",
                table: "Incidents",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Camera#1" },
                    { 2, "Camera#2" },
                    { 3, "Camera#3" }
                });

            migrationBuilder.InsertData(
                table: "GroupPresets",
                columns: new[] { "ID", "DateCreated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preset#1" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preset#2" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preset#3" }
                });
        }
    }
}
