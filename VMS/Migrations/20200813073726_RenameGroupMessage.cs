using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class RenameGroupMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAssignments_GroupMessages_GroupMessageID",
                table: "MessageAssignments");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "PushNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAssignments",
                table: "MessageAssignments");

            migrationBuilder.DropIndex(
                name: "IX_MessageAssignments_GroupMessageID",
                table: "MessageAssignments");

            migrationBuilder.DropColumn(
                name: "GroupMessageID",
                table: "MessageAssignments");

            migrationBuilder.AddColumn<int>(
                name: "EditorMessageID",
                table: "MessageAssignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAssignments",
                table: "MessageAssignments",
                columns: new[] { "DisplayBoardID", "EditorMessageID" });

            migrationBuilder.CreateTable(
                name: "EditorMessages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Message = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Timer = table.Column<int>(nullable: true),
                    CodePlus = table.Column<string>(nullable: true),
                    ThisBoard = table.Column<int>(nullable: false),
                    EditorTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EditorMessages_EditorType_EditorTypeID",
                        column: x => x.EditorTypeID,
                        principalTable: "EditorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_EditorMessageID",
                table: "MessageAssignments",
                column: "EditorMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_EditorMessages_EditorTypeID",
                table: "EditorMessages",
                column: "EditorTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAssignments_EditorMessages_EditorMessageID",
                table: "MessageAssignments",
                column: "EditorMessageID",
                principalTable: "EditorMessages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAssignments_EditorMessages_EditorMessageID",
                table: "MessageAssignments");

            migrationBuilder.DropTable(
                name: "EditorMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAssignments",
                table: "MessageAssignments");

            migrationBuilder.DropIndex(
                name: "IX_MessageAssignments_EditorMessageID",
                table: "MessageAssignments");

            migrationBuilder.DropColumn(
                name: "EditorMessageID",
                table: "MessageAssignments");

            migrationBuilder.AddColumn<int>(
                name: "GroupMessageID",
                table: "MessageAssignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAssignments",
                table: "MessageAssignments",
                columns: new[] { "DisplayBoardID", "GroupMessageID" });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CodePlus = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorTypeID = table.Column<int>(type: "integer", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    InfoProviderID = table.Column<int>(type: "integer", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    ThisBoard = table.Column<int>(type: "integer", nullable: false),
                    Timer = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupMessages_EditorType_EditorTypeID",
                        column: x => x.EditorTypeID,
                        principalTable: "EditorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessages_InfoProviders_InfoProviderID",
                        column: x => x.InfoProviderID,
                        principalTable: "InfoProviders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BoardId = table.Column<int>(type: "integer", nullable: false),
                    Notification = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_GroupMessageID",
                table: "MessageAssignments",
                column: "GroupMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_EditorTypeID",
                table: "GroupMessages",
                column: "EditorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_InfoProviderID",
                table: "GroupMessages",
                column: "InfoProviderID");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAssignments_GroupMessages_GroupMessageID",
                table: "MessageAssignments",
                column: "GroupMessageID",
                principalTable: "GroupMessages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
