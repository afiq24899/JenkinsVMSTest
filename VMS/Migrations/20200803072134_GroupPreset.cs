using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class GroupPreset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CameraName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupPresets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPresets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BoardGroupingAssignments",
                columns: table => new
                {
                    GroupPresetID = table.Column<int>(nullable: false),
                    BoardID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGroupingAssignments", x => new { x.GroupPresetID, x.BoardID });
                    table.ForeignKey(
                        name: "FK_BoardGroupingAssignments_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGroupingAssignments_GroupPresets_GroupPresetID",
                        column: x => x.GroupPresetID,
                        principalTable: "GroupPresets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CameraGroupingAssignments",
                columns: table => new
                {
                    GroupPresetID = table.Column<int>(nullable: false),
                    CameraID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraGroupingAssignments", x => new { x.GroupPresetID, x.CameraID });
                    table.ForeignKey(
                        name: "FK_CameraGroupingAssignments_Cameras_CameraID",
                        column: x => x.CameraID,
                        principalTable: "Cameras",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CameraGroupingAssignments_GroupPresets_GroupPresetID",
                        column: x => x.GroupPresetID,
                        principalTable: "GroupPresets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGroupingAssignments_BoardID",
                table: "BoardGroupingAssignments",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_CameraGroupingAssignments_CameraID",
                table: "CameraGroupingAssignments",
                column: "CameraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGroupingAssignments");

            migrationBuilder.DropTable(
                name: "CameraGroupingAssignments");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "GroupPresets");
        }
    }
}
