﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetroFront.DataAccess;

namespace RetroFront.DataAccess.Migrations
{
    [DbContext(typeof(AppSQLIteContext))]
    [Migration("20210115211433_rename_game")]
    partial class rename_game
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasIndex("SystemeID");

                    b.ToTable("Emulators");
                });

            modelBuilder.Entity("RetroFront.Models.GameRom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Banner")
                        .HasColumnType("TEXT");

                    b.Property<string>("Box3dart")
                        .HasColumnType("TEXT");

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

                    b.Property<string>("Logo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("EmulatorID");

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

                    b.HasKey("SystemeID");

                    b.ToTable("Systemes");
                });

            modelBuilder.Entity("RetroFront.Models.Emulator", b =>
                {
                    b.HasOne("RetroFront.Models.Systeme", "Systeme")
                        .WithMany("Emulators")
                        .HasForeignKey("SystemeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Systeme");
                });

            modelBuilder.Entity("RetroFront.Models.GameRom", b =>
                {
                    b.HasOne("RetroFront.Models.Emulator", "Emulator")
                        .WithMany("Games")
                        .HasForeignKey("EmulatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emulator");
                });

            modelBuilder.Entity("RetroFront.Models.Emulator", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("RetroFront.Models.Systeme", b =>
                {
                    b.Navigation("Emulators");
                });
#pragma warning restore 612, 618
        }
    }
}
