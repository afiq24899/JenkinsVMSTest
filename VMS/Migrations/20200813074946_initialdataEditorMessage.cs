using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class initialdataEditorMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EditorMessages",
                columns: new[] { "ID", "CodePlus", "Date", "EditorTypeID", "ImageFileName", "Message", "ThisBoard", "Timer" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "message1", 0, null },
                    { 2, null, null, null, null, "message2", 0, null },
                    { 3, null, null, null, null, "message3", 0, null },
                    { 4, null, null, null, null, "message4", 0, null },
                    { 5, null, null, null, null, "message5", 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EditorMessages",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EditorMessages",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EditorMessages",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EditorMessages",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EditorMessages",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
