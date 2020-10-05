using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditorType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    H_Name = table.Column<string>(nullable: true),
                    H_Address = table.Column<string>(nullable: true),
                    H_User = table.Column<string>(nullable: true),
                    H_NowDateTime = table.Column<DateTime>(nullable: false),
                    Object = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EventClass = table.Column<string>(nullable: true),
                    BoardID = table.Column<string>(nullable: true),
                    Bname = table.Column<string>(nullable: true),
                    EventImage = table.Column<string>(nullable: true),
                    ExtraImage = table.Column<string>(nullable: true),
                    DateTimeReceived = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "InfoProviders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Provider = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoProviders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingInfos_B1s",
                columns: table => new
                {
                    MallID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Board = table.Column<string>(nullable: true),
                    Phase = table.Column<int>(nullable: false),
                    Bname = table.Column<string>(nullable: true),
                    sname = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parking = table.Column<string>(nullable: true),
                    NowDateTime = table.Column<DateTime>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingInfos_B1s", x => x.MallID);
                });

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BoardId = table.Column<int>(nullable: false),
                    Notification = table.Column<bool>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Year = table.Column<string>(nullable: true),
                    Months = table.Column<string>(nullable: true),
                    Days = table.Column<string>(nullable: true),
                    Boards = table.Column<string>(nullable: true),
                    UpTotal = table.Column<string>(nullable: true),
                    DownTotal = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TravelTimeInfos_B1s",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    eventType = table.Column<string>(nullable: true),
                    sname = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    datetime = table.Column<string>(nullable: true),
                    eta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTimeInfos_B1s", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UptimeReports",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UpTime = table.Column<int>(nullable: false),
                    DownTime = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    DailyUptime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UptimeReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VidType = table.Column<string>(nullable: true),
                    BoardID = table.Column<string>(nullable: true),
                    Bname = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    NowDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.MessageID);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Message = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Timer = table.Column<int>(nullable: true),
                    EditorTypeID = table.Column<int>(nullable: true),
                    CodePlus = table.Column<string>(nullable: true),
                    ThisBoard = table.Column<int>(nullable: false),
                    InfoProviderID = table.Column<int>(nullable: true)
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
                name: "ParkingInfos",
                columns: table => new
                {
                    InfoProviderID = table.Column<int>(nullable: false),
                    Board = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    ParkingBay = table.Column<int>(nullable: false),
                    NowDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingInfos", x => x.InfoProviderID);
                    table.ForeignKey(
                        name: "FK_ParkingInfos_InfoProviders_InfoProviderID",
                        column: x => x.InfoProviderID,
                        principalTable: "InfoProviders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrafficInfos",
                columns: table => new
                {
                    InfoProviderID = table.Column<int>(nullable: false),
                    Board = table.Column<string>(nullable: true),
                    Event = table.Column<string>(nullable: true),
                    TravelTime = table.Column<string>(nullable: true),
                    PointA = table.Column<string>(nullable: true),
                    PointB = table.Column<string>(nullable: true),
                    NowDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficInfos", x => x.InfoProviderID);
                    table.ForeignKey(
                        name: "FK_TrafficInfos_InfoProviders_InfoProviderID",
                        column: x => x.InfoProviderID,
                        principalTable: "InfoProviders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ZoneID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Boards_Zones_ZoneID",
                        column: x => x.ZoneID,
                        principalTable: "Zones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Displays",
                columns: table => new
                {
                    BoardID = table.Column<int>(nullable: false),
                    C4_IP = table.Column<string>(nullable: true),
                    PowerStatus = table.Column<bool>(nullable: false),
                    Alert = table.Column<bool>(nullable: false),
                    Screenshot = table.Column<string>(nullable: true),
                    CCTV = table.Column<string>(nullable: true),
                    InstallationDate = table.Column<DateTime>(nullable: false),
                    OperationalStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Displays", x => x.BoardID);
                    table.ForeignKey(
                        name: "FK_Displays_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    BoardID = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.BoardID);
                    table.ForeignKey(
                        name: "FK_Locations_Boards_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Boards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAssignments",
                columns: table => new
                {
                    DisplayBoardID = table.Column<int>(nullable: false),
                    GroupMessageID = table.Column<int>(nullable: false),
                    ImageFileName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Timer = table.Column<int>(nullable: true),
                    EditorTypeID = table.Column<int>(nullable: true),
                    CodePlus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAssignments", x => new { x.DisplayBoardID, x.GroupMessageID });
                    table.ForeignKey(
                        name: "FK_MessageAssignments_Displays_DisplayBoardID",
                        column: x => x.DisplayBoardID,
                        principalTable: "Displays",
                        principalColumn: "BoardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageAssignments_EditorType_EditorTypeID",
                        column: x => x.EditorTypeID,
                        principalTable: "EditorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageAssignments_GroupMessages_GroupMessageID",
                        column: x => x.GroupMessageID,
                        principalTable: "GroupMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ZoneID",
                table: "Boards",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_EditorTypeID",
                table: "GroupMessages",
                column: "EditorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_InfoProviderID",
                table: "GroupMessages",
                column: "InfoProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_EditorTypeID",
                table: "MessageAssignments",
                column: "EditorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAssignments_GroupMessageID",
                table: "MessageAssignments",
                column: "GroupMessageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MessageAssignments");

            migrationBuilder.DropTable(
                name: "ParkingInfos");

            migrationBuilder.DropTable(
                name: "ParkingInfos_B1s");

            migrationBuilder.DropTable(
                name: "PushNotifications");

            migrationBuilder.DropTable(
                name: "ReportData");

            migrationBuilder.DropTable(
                name: "TrafficInfos");

            migrationBuilder.DropTable(
                name: "TravelTimeInfos_B1s");

            migrationBuilder.DropTable(
                name: "UptimeReports");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Displays");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "EditorType");

            migrationBuilder.DropTable(
                name: "InfoProviders");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
