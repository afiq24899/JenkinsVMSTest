using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class NewTableIncidentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncidentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "IncidentTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Accident" },
                    { 2, "Congestion" },
                    { 3, "Illegal_Stop" },
                    { 4, "Person_on_Lane" },
                    { 5, "Bad_Weather" },
                    { 6, "Retrograde" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentTypes");
        }
    }
}
