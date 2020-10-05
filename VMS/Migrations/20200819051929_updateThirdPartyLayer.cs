using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class updateThirdPartyLayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InfoTypeID",
                table: "IncidentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InfoTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "InfoTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Incident" });

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 2,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 4,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 5,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IncidentTypes",
                keyColumn: "ID",
                keyValue: 6,
                column: "InfoTypeID",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_IncidentTypes_InfoTypeID",
                table: "IncidentTypes",
                column: "InfoTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentTypes_InfoTypes_InfoTypeID",
                table: "IncidentTypes",
                column: "InfoTypeID",
                principalTable: "InfoTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentTypes_InfoTypes_InfoTypeID",
                table: "IncidentTypes");

            migrationBuilder.DropTable(
                name: "InfoTypes");

            migrationBuilder.DropIndex(
                name: "IX_IncidentTypes_InfoTypeID",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "InfoTypeID",
                table: "IncidentTypes");
        }
    }
}
