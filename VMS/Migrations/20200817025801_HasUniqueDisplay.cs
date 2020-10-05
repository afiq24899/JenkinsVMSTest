using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingkail.VMS.Migrations
{
    public partial class HasUniqueDisplay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasUniqueDisplay",
                table: "Displays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUniqueDisplay",
                table: "Displays");
        }
    }
}
