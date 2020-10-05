using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class dataDBKLPGIS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkingInfos",
                columns: new[] { "MallID", "Bname", "Board", "ImageFileName", "NowDateTime", "Phase", "name", "parking", "sname" },
                values: new object[,]
                {
                    { 1, "V001", "1", "sungeiwang_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "SUNGEI WANG PLAZA", "OPEN", "SGWANG" },
                    { 2, "V001", "1", "lowyat_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "PLAZA LOW YAT", "0787", "LOWYAT" },
                    { 3, "V001", "1", "Lot10_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "LOT 10", "CLOSE", "LOT10" },
                    { 4, "V001", "1", "fahrenheit_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "FAHRENHEIT88", "5555", "FAHRENHEIT" },
                    { 5, "V001", "1", "starhill_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "STARHILL GALLERY", "0659", "STARHILL" },
                    { 6, "V001", "1", "pavilion_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "PAVILION", "OPEN", "PAVILION" },
                    { 7, "V001", "1", "KLCC_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "KLCC", "6940", "KLCC" },
                    { 8, "V001", "1", "semuahouse_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "SEMUA HOUSE", "OPEN", "SEMUAHOUSE" },
                    { 9, "V001", "1", "pt80_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "PT80", "OPEN", "PT80" },
                    { 10, "V001", "1", "capsquare_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "CAPITAL SQUARE", "OPEN", "CAPSQUARE" },
                    { 11, "V001", "1", "pertamacomplex_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "PERTAMA COMPLEX", "OPEN", "PERTAMA" },
                    { 12, "V001", "1", "sogo_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "SOGO", "OPEN", "SOGO" },
                    { 13, "V001", "1", "quill_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "QUILL CITY MALL", "OPEN", "QUILLCITY" },
                    { 14, "V001", "1", "", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "NULL", "OPEN", "MALL14" },
                    { 15, "V001", "1", "MajuJunction_192x64", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, "PARKSON MAJU JUNCTION", "OPEN", "MAJUJUNCTION" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ParkingInfos",
                keyColumn: "MallID",
                keyValue: 15);
        }
    }
}
