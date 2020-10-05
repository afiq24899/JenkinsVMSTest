using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class newEditorMessageType5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EditorMessageType",
                columns: new[] { "ID", "Type" },
                values: new object[] { 5, "Weather" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EditorMessageType",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
