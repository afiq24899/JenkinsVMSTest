using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class dataEditorMsgType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EditorMessageType",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "Parking" },
                    { 2, "Upload" },
                    { 3, "Traveltime" },
                    { 4, "Custom" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EditorMessageType",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EditorMessageType",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EditorMessageType",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EditorMessageType",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
