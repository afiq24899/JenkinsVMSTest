﻿// <auto-generated />
using System;
using Lingkail.VMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lingkail.VMS.Migrations
{
    [DbContext(typeof(SenaVMSContext))]
    [Migration("20200724160018_changetypeofcolumn")]
    partial class changetypeofcolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Lingkail.VMS.Models.Board", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ZoneID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ZoneID");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Display", b =>
                {
                    b.Property<int>("BoardID")
                        .HasColumnType("integer");

                    b.Property<bool>("Alert")
                        .HasColumnType("boolean");

                    b.Property<string>("C4_IP")
                        .HasColumnType("text");

                    b.Property<string>("CCTV")
                        .HasColumnType("text");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OperationalStatus")
                        .HasColumnType("integer");

                    b.Property<bool>("PowerStatus")
                        .HasColumnType("boolean");

                    b.Property<string>("Screenshot")
                        .HasColumnType("text");

                    b.HasKey("BoardID");

                    b.ToTable("Displays");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.EditorType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("EditorType");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.GroupMessage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CodePlus")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("EditorTypeID")
                        .HasColumnType("integer");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("text");

                    b.Property<int?>("InfoProviderID")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("ThisBoard")
                        .HasColumnType("integer");

                    b.Property<int?>("Timer")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("EditorTypeID");

                    b.HasIndex("InfoProviderID");

                    b.ToTable("GroupMessages");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.History", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("H_Address")
                        .HasColumnType("text");

                    b.Property<string>("H_Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("H_NowDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("H_User")
                        .HasColumnType("text");

                    b.Property<string>("Object")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.InfoProvider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Provider")
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("InfoProviders");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Location", b =>
                {
                    b.Property<int>("BoardID")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.HasKey("BoardID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.MessageAssignment", b =>
                {
                    b.Property<int>("DisplayBoardID")
                        .HasColumnType("integer");

                    b.Property<int>("GroupMessageID")
                        .HasColumnType("integer");

                    b.Property<string>("CodePlus")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("EditorTypeID")
                        .HasColumnType("integer");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("text");

                    b.Property<int?>("Timer")
                        .HasColumnType("integer");

                    b.HasKey("DisplayBoardID", "GroupMessageID");

                    b.HasIndex("EditorTypeID");

                    b.HasIndex("GroupMessageID");

                    b.ToTable("MessageAssignments");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.ParkingInfo", b =>
                {
                    b.Property<int>("InfoProviderID")
                        .HasColumnType("integer");

                    b.Property<string>("Board")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<DateTime?>("NowDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ParkingBay")
                        .HasColumnType("integer");

                    b.HasKey("InfoProviderID");

                    b.ToTable("ParkingInfos");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.ParkingInfos_B1s", b =>
                {
                    b.Property<int>("MallID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Bname")
                        .HasColumnType("text");

                    b.Property<string>("Board")
                        .HasColumnType("text");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("NowDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Phase")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("parking")
                        .HasColumnType("text");

                    b.Property<string>("sname")
                        .HasColumnType("text");

                    b.HasKey("MallID");

                    b.ToTable("ParkingInfos_B1s");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.PushNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("BoardId")
                        .HasColumnType("integer");

                    b.Property<bool>("Notification")
                        .HasColumnType("boolean");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.ToTable("PushNotifications");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.ReportData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Boards")
                        .HasColumnType("text");

                    b.Property<string>("Days")
                        .HasColumnType("text");

                    b.Property<string>("DownTotal")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Months")
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.Property<string>("StartDate")
                        .HasColumnType("text");

                    b.Property<string>("UpTotal")
                        .HasColumnType("text");

                    b.Property<string>("Year")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("ReportData");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Services+Incident", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Bname")
                        .HasColumnType("text");

                    b.Property<string>("BoardID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTimeReceived")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EventClass")
                        .HasColumnType("text");

                    b.Property<string>("EventImage")
                        .HasColumnType("text");

                    b.Property<string>("ExtraImage")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("EventID");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Services+Video", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Bname")
                        .HasColumnType("text");

                    b.Property<string>("BoardID")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("NowDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("VidType")
                        .HasColumnType("text");

                    b.HasKey("MessageID");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.TrafficInfo", b =>
                {
                    b.Property<int>("InfoProviderID")
                        .HasColumnType("integer");

                    b.Property<string>("Board")
                        .HasColumnType("text");

                    b.Property<string>("Event")
                        .HasColumnType("text");

                    b.Property<DateTime?>("NowDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PointA")
                        .HasColumnType("text");

                    b.Property<string>("PointB")
                        .HasColumnType("text");

                    b.Property<string>("TravelTime")
                        .HasColumnType("text");

                    b.HasKey("InfoProviderID");

                    b.ToTable("TrafficInfos");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.TravelTimeInfos_B1s", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("datetime")
                        .HasColumnType("text");

                    b.Property<string>("eta")
                        .HasColumnType("text");

                    b.Property<string>("eventType")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("sname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("TravelTimeInfos_B1s");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.UptimeReport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.Property<string>("TimeEnd")
                        .HasColumnType("text");

                    b.Property<string>("TimeStart")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("UptimeReports");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Zone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Board", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.Zone", "Zone")
                        .WithMany("Boards")
                        .HasForeignKey("ZoneID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Display", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.Board", "Board")
                        .WithOne("Display")
                        .HasForeignKey("Lingkail.VMS.Models.Display", "BoardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingkail.VMS.Models.GroupMessage", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.EditorType", "EditorType")
                        .WithMany("GroupMessages")
                        .HasForeignKey("EditorTypeID");

                    b.HasOne("Lingkail.VMS.Models.InfoProvider", "InfoProvider")
                        .WithMany("GroupMessages")
                        .HasForeignKey("InfoProviderID");
                });

            modelBuilder.Entity("Lingkail.VMS.Models.Location", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.Board", "Board")
                        .WithOne("Location")
                        .HasForeignKey("Lingkail.VMS.Models.Location", "BoardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingkail.VMS.Models.MessageAssignment", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.Display", "Display")
                        .WithMany("MessageAssignments")
                        .HasForeignKey("DisplayBoardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingkail.VMS.Models.EditorType", "EditorType")
                        .WithMany("MessageAssignments")
                        .HasForeignKey("EditorTypeID");

                    b.HasOne("Lingkail.VMS.Models.GroupMessage", "GroupMessage")
                        .WithMany("MessageAssignments")
                        .HasForeignKey("GroupMessageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingkail.VMS.Models.ParkingInfo", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.InfoProvider", "InfoProvider")
                        .WithOne("ParkingInfo")
                        .HasForeignKey("Lingkail.VMS.Models.ParkingInfo", "InfoProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingkail.VMS.Models.TrafficInfo", b =>
                {
                    b.HasOne("Lingkail.VMS.Models.InfoProvider", "InfoProvider")
                        .WithOne("TrafficInfo")
                        .HasForeignKey("Lingkail.VMS.Models.TrafficInfo", "InfoProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
