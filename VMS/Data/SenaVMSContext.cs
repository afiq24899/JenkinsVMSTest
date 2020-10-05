using Microsoft.EntityFrameworkCore;
using Lingkail.VMS.Models;
using System;
using static Lingkail.VMS.Models.Services;

namespace Lingkail.VMS.Data
{
    public class SenaVMSContext : DbContext
    {
        public SenaVMSContext(DbContextOptions<SenaVMSContext> options)
            : base(options) { }


        public DbSet<Board> Boards { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Display> Displays { get; set; }
        public DbSet<EditorMessage> EditorMessages { get; set; }
        public DbSet<MessageAssignment> MessageAssignments { get; set; }


        public DbSet<GroupPreset> GroupPresets { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<BoardGroupingAssignment> BoardGroupingAssignments { get; set; }
        public DbSet<CameraGroupingAssignment> CameraGroupingAssignments { get; set; }

        
        public DbSet<InfoType> InfoTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentType> IncidentTypes { get; set; }


        public DbSet<TrafficInfo> TrafficInfos { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<DBKL_PGIS> ParkingInfos { get; set; }
        public DbSet<TravelTimeInfos_B1s> TravelTimeInfos_B1s { get; set; }
        public DbSet<VMS.Models.Services.Video> Videos { get; set; }

        public DbSet<UptimeReport> UptimeReports { get; set; }
        public DbSet<ReportData> ReportData { get; set; }
        public DbSet<WeatherForecast> WeatherForecast { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite PK configurated with fluent API (many-to-many)
            modelBuilder.Entity<MessageAssignment>()
                .HasKey(ma => new { ma.DisplayBoardID, ma.EditorMessageID });
            modelBuilder.Entity<BoardGroupingAssignment>()
                .HasKey(gb => new { gb.GroupPresetID, gb.BoardID  });
            modelBuilder.Entity<CameraGroupingAssignment>()
                .HasKey(gb => new { gb.GroupPresetID, gb.CameraID });
            modelBuilder.Entity<CameraGroupingAssignment>()
                .HasKey(gb => new { gb.GroupPresetID, gb.CameraID });

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder _modelBuilder) 
        {
            #region Camera
            _modelBuilder.Entity<Camera>()
                .HasData(
                new Camera { ID = 1, Name = "Camera#1" },
                new Camera { ID = 2, Name = "Camera#2" },
                new Camera { ID = 3, Name = "Camera#3" }
                );
            #endregion

            #region EditorMessage
            _modelBuilder.Entity<EditorMessage>()
            .HasData(
                new EditorMessage { ID = 1, Message = "message1" },
                new EditorMessage { ID = 2, Message = "message2" },
                new EditorMessage { ID = 3, Message = "message3" },
                new EditorMessage { ID = 4, Message = "message4" },
                new EditorMessage { ID = 5, Message = "message5" }
            );
            #endregion

            #region EditorMessageType
            _modelBuilder.Entity<EditorMessageType>()
            .HasData(
                new EditorMessageType { ID = 1, Type = "Parking" },
                new EditorMessageType { ID = 2, Type = "Upload" },
                new EditorMessageType { ID = 3, Type = "Traveltime" },
                new EditorMessageType { ID = 4, Type = "Custom" },
                new EditorMessageType { ID = 5, Type = "Weather" }
            );
            #endregion

            #region InfoType
            _modelBuilder.Entity<InfoType>()
                .HasData(
                new InfoType { ID = 1, Name = "IncidentType" }
                );
            #endregion

            #region IncidentType
            _modelBuilder.Entity<IncidentType>()
                .HasData(
                new IncidentType { ID = 1, Name = "Accident" },
                new IncidentType { ID = 2, Name = "Congestion" },
                new IncidentType { ID = 3, Name = "Illegal_Stop" },
                new IncidentType { ID = 4, Name = "Person_on_Lane" },
                new IncidentType { ID = 5, Name = "Bad_Weather" },
                new IncidentType { ID = 6, Name = "Retrograde" }
                );
            #endregion

            #region DBKL_PGIS
            _modelBuilder.Entity<DBKL_PGIS>()
                .HasData(
                    new DBKL_PGIS
                    {
                        MallID = 1,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "SGWANG",
                        name = "SUNGEI WANG PLAZA",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "sungeiwang_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 2,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "LOWYAT",
                        name = "PLAZA LOW YAT",
                        parking = "0787",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "lowyat_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 3,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "LOT10",
                        name = "LOT 10",
                        parking = "CLOSE",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "Lot10_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 4,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "FAHRENHEIT",
                        name = "FAHRENHEIT88",
                        parking = "5555",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "fahrenheit_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 5,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "STARHILL",
                        name = "STARHILL GALLERY",
                        parking = "0659",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "starhill_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 6,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "PAVILION",
                        name = "PAVILION",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "pavilion_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 7,
                        Board = "1",
                        Phase = 1,
                        Bname = "V001",
                        sname = "KLCC",
                        name = "KLCC",
                        parking = "6940",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "KLCC_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 8,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "SEMUAHOUSE",
                        name = "SEMUA HOUSE",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "semuahouse_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 9,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "PT80",
                        name = "PT80",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "pt80_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 10,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "CAPSQUARE",
                        name = "CAPITAL SQUARE",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "capsquare_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 11,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "PERTAMA",
                        name = "PERTAMA COMPLEX",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "pertamacomplex_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 12,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "SOGO",
                        name = "SOGO",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "sogo_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 13,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "QUILLCITY",
                        name = "QUILL CITY MALL",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "quill_192x64"
                    },
                    new DBKL_PGIS
                    {
                        MallID = 14,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "MALL14",
                        name = "NULL",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = ""
                    },
                    new DBKL_PGIS
                    {
                        MallID = 15,
                        Board = "1",
                        Phase = 2,
                        Bname = "V001",
                        sname = "MAJUJUNCTION",
                        name = "PARKSON MAJU JUNCTION",
                        parking = "OPEN",
                        NowDateTime = new DateTime(2020, 01, 01, 12, 00, 00),
                        ImageFileName = "MajuJunction_192x64"
                    }
                );
            #endregion
        }

    }
}

