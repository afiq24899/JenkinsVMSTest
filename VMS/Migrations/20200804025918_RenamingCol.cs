using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class RenamingCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "GroupPresets");

            migrationBuilder.DropColumn(
                name: "CameraName",
                table: "Cameras");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroupPresets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cameras",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Camera#1");

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Camera#2");

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Camera#3");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Preset#1");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Preset#2");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Preset#3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroupPresets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cameras");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "GroupPresets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CameraName",
                table: "Cameras",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 1,
                column: "CameraName",
                value: "Camera#1");

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 2,
                column: "CameraName",
                value: "Camera#2");

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "ID",
                keyValue: 3,
                column: "CameraName",
                value: "Camera#3");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 1,
                column: "GroupName",
                value: "Preset#1");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 2,
                column: "GroupName",
                value: "Preset#2");

            migrationBuilder.UpdateData(
                table: "GroupPresets",
                keyColumn: "ID",
                keyValue: 3,
                column: "GroupName",
                value: "Preset#3");
        }
    }
}
