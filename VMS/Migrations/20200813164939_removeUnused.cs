using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class removeUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingInfos_InfoProviders_InfoProviderID",
                table: "ParkingInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TrafficInfos_InfoProviders_InfoProviderID",
                table: "TrafficInfos");

            migrationBuilder.DropTable(
                name: "InfoProviders");

            migrationBuilder.DropTable(
                name: "ParkingInfos_B1s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingInfos",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "InfoProviderID",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "ParkingBay",
                table: "ParkingInfos");

            migrationBuilder.AlterColumn<int>(
                name: "InfoProviderID",
                table: "TrafficInfos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "MallID",
                table: "ParkingInfos",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "Bname",
                table: "ParkingInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "ParkingInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Phase",
                table: "ParkingInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ParkingInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "parking",
                table: "ParkingInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sname",
                table: "ParkingInfos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingInfos",
                table: "ParkingInfos",
                column: "MallID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingInfos",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "MallID",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "Bname",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "Phase",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "parking",
                table: "ParkingInfos");

            migrationBuilder.DropColumn(
                name: "sname",
                table: "ParkingInfos");

            migrationBuilder.AlterColumn<int>(
                name: "InfoProviderID",
                table: "TrafficInfos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "InfoProviderID",
                table: "ParkingInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ParkingInfos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingBay",
                table: "ParkingInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingInfos",
                table: "ParkingInfos",
                column: "InfoProviderID");

            migrationBuilder.CreateTable(
                name: "InfoProviders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Provider = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoProviders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingInfos_B1s",
                columns: table => new
                {
                    MallID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bname = table.Column<string>(type: "text", nullable: true),
                    Board = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    NowDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Phase = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    parking = table.Column<string>(type: "text", nullable: true),
                    sname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingInfos_B1s", x => x.MallID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingInfos_InfoProviders_InfoProviderID",
                table: "ParkingInfos",
                column: "InfoProviderID",
                principalTable: "InfoProviders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrafficInfos_InfoProviders_InfoProviderID",
                table: "TrafficInfos",
                column: "InfoProviderID",
                principalTable: "InfoProviders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
