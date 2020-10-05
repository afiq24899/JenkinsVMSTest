using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class renameEditorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditorMessages_EditorType_EditorTypeID",
                table: "EditorMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageAssignments_EditorType_EditorTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropTable(
                name: "EditorType");

            migrationBuilder.DropIndex(
                name: "IX_MessageAssignments_EditorTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EditorMessages_EditorTypeID",
                table: "EditorMessages");

            migrationBuilder.DropColumn(
                name: "EditorTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropColumn(
                name: "EditorTypeID",
                table: "EditorMessages");

            migrationBuilder.AddColumn<int>(
                name: "EditorMessageTypeID",
                table: "MessageAssignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorMessageTypeID",
                table: "EditorMessages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditorMessageType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorMessageType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_EditorMessageTypeID",
                table: "MessageAssignments",
                column: "EditorMessageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EditorMessages_EditorMessageTypeID",
                table: "EditorMessages",
                column: "EditorMessageTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EditorMessages_EditorMessageType_EditorMessageTypeID",
                table: "EditorMessages",
                column: "EditorMessageTypeID",
                principalTable: "EditorMessageType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAssignments_EditorMessageType_EditorMessageTypeID",
                table: "MessageAssignments",
                column: "EditorMessageTypeID",
                principalTable: "EditorMessageType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditorMessages_EditorMessageType_EditorMessageTypeID",
                table: "EditorMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageAssignments_EditorMessageType_EditorMessageTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropTable(
                name: "EditorMessageType");

            migrationBuilder.DropIndex(
                name: "IX_MessageAssignments_EditorMessageTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EditorMessages_EditorMessageTypeID",
                table: "EditorMessages");

            migrationBuilder.DropColumn(
                name: "EditorMessageTypeID",
                table: "MessageAssignments");

            migrationBuilder.DropColumn(
                name: "EditorMessageTypeID",
                table: "EditorMessages");

            migrationBuilder.AddColumn<int>(
                name: "EditorTypeID",
                table: "MessageAssignments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorTypeID",
                table: "EditorMessages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditorType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_EditorTypeID",
                table: "MessageAssignments",
                column: "EditorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EditorMessages_EditorTypeID",
                table: "EditorMessages",
                column: "EditorTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EditorMessages_EditorType_EditorTypeID",
                table: "EditorMessages",
                column: "EditorTypeID",
                principalTable: "EditorType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAssignments_EditorType_EditorTypeID",
                table: "MessageAssignments",
                column: "EditorTypeID",
                principalTable: "EditorType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
