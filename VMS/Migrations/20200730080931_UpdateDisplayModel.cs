using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class UpdateDisplayModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alert",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "PowerStatus",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "Screenshot",
                table: "Displays");

            migrationBuilder.AddColumn<bool>(
                name: "HasAlert",
                table: "Displays",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Displays",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Screenshot_URL",
                table: "Displays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAlert",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "Screenshot_URL",
                table: "Displays");

            migrationBuilder.AddColumn<bool>(
                name: "Alert",
                table: "Displays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PowerStatus",
                table: "Displays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Screenshot",
                table: "Displays",
                type: "text",
                nullable: true);
        }
    }
}
