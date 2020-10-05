using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class updateThirdPartyLayer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentTypes_InfoTypes_InfoTypeID",
                table: "IncidentTypes");

            migrationBuilder.DropIndex(
                name: "IX_IncidentTypes_InfoTypeID",
                table: "IncidentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents");

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Incidents",
                keyColumn: "EventID",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "InfoTypeID",
                table: "IncidentTypes");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Bname",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "BoardID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "DateTimePushed",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "EventClass",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ExtraImage",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "IsFullPath",
                table: "Incidents");

            migrationBuilder.AddColumn<string>(
                name: "Object",
                table: "InfoTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Incidents",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeReceived",
                table: "Incidents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IncidentTypeID",
                table: "Incidents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullWebPath",
                table: "Incidents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ThisBoardId",
                table: "Incidents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThisBoardName",
                table: "Incidents",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "InfoTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "IncidentType");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IncidentTypeID",
                table: "Incidents",
                column: "IncidentTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeID",
                table: "Incidents",
                column: "IncidentTypeID",
                principalTable: "IncidentTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_IncidentTypes_IncidentTypeID",
                table: "Incidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_IncidentTypeID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Object",
                table: "InfoTypes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "DateTimeReceived",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "IncidentTypeID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "IsFullWebPath",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ThisBoardId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ThisBoardName",
                table: "Incidents");

            migrationBuilder.AddColumn<int>(
                name: "InfoTypeID",
                table: "IncidentTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Incidents",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "Bname",
                table: "Incidents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoardID",
                table: "Incidents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimePushed",
                table: "Incidents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EventClass",
                table: "Incidents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraImage",
                table: "Incidents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullPath",
                table: "Incidents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidents",
                table: "Incidents",
                column: "EventID");

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

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "EventID", "Bname", "BoardID", "DateTimePushed", "EventClass", "EventImage", "ExtraImage", "IsFullPath", "Text" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accident", null, null, false, null },
                    { 2, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Congestion", null, null, false, null },
                    { 3, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Illegal_Stop", null, null, false, null },
                    { 4, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Person_on_Lane", null, null, false, null },
                    { 5, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bad_Weather", null, null, false, null },
                    { 6, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Retrograde", null, null, false, null }
                });

            migrationBuilder.UpdateData(
                table: "InfoTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Incident");

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
    }
}
