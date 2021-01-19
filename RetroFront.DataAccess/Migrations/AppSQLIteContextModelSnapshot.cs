﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetroFront.DataAccess;

namespace RetroFront.DataAccess.Migrations
{
    [DbContext(typeof(AppSQLIteContext))]
    partial class AppSQLIteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RetroFront.Models.Emulator", b =>
                {
                    b.Property<int>("EmulatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Chemin")
                        .HasColumnType("TEXT");

                    b.Property<string>("Command")
                        .HasColumnType("TEXT");

                    b.Property<string>("Extension")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SystemeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmulatorID");

                    b.ToTable("Emulators");
                });

            modelBuilder.Entity("RetroFront.Models.GameRom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Boxart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dev")
                        .HasColumnType("TEXT");

                    b.Property<string>("Editeur")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmulatorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fanart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .HasColumnType("TEXT");

                    b.Property<string>("Logo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecalView")
                        .HasColumnType("TEXT");

                    b.Property<string>("Screenshoot")
                        .HasColumnType("TEXT");

                    b.Property<int>("SteamID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TitleScreen")
                        .HasColumnType("TEXT");

                    b.Property<string>("Video")
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RetroFront.Models.Systeme", b =>
                {
                    b.Property<int>("SystemeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Shortname")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("SystemeID");

                    b.ToTable("Systemes");
                });
#pragma warning restore 612, 618
        }
    }
}
