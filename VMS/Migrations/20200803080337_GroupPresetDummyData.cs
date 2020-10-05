using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class GroupPresetDummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "ID", "CameraName" },
                values: new object[,]
                {
                    { 1, "Camera#1" },
                    { 2, "Camera#2" },
                    { 3, "Camera#3" }
                });

            migrationBuilder.InsertData(
                table: "GroupPresets",
                columns: new[] { "ID", "GroupName" },
                values: new object[,]
                {
                    { 1, "Preset#1" },
                    { 2, "Preset#2" },
                    { 3, "Preset#3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
