using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class seedIncident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
