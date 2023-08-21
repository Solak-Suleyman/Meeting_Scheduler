﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication4.Models.Context;

#nullable disable

namespace WebApplication4.Migrations
{
    [DbContext(typeof(MeetingSchedulerContext))]
    partial class MeetingSchedulerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication4.Models.Entity.Meeting", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("created_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("from_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("to_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("updated_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("id");

                    b.HasIndex("RoomId");

                    b.ToTable("meetings", "public");
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("created_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<char>("status")
                        .HasColumnType("character(1)");

                    b.Property<DateTime>("updated_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("rooms", "public");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            created_time = new DateTime(2023, 8, 21, 9, 10, 3, 951, DateTimeKind.Utc).AddTicks(8589),
                            name = "A1",
                            status = 'A',
                            updated_time = new DateTime(2023, 8, 21, 9, 10, 3, 951, DateTimeKind.Utc).AddTicks(8589)
                        });
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("created_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<char>("status")
                        .HasColumnType("character(1)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updated_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("users", "public");

                    b.HasData(
                        new
                        {
                            id = 1,
                            created_time = new DateTime(2023, 8, 21, 9, 10, 3, 951, DateTimeKind.Utc).AddTicks(8503),
                            name = "Süleyman",
                            password = "cRDtpNCeBiql5KOQsKVyrA0sAiA=",
                            status = 'A',
                            surname = "Solak",
                            updated_time = new DateTime(2023, 8, 21, 9, 10, 3, 951, DateTimeKind.Utc).AddTicks(8504),
                            user_name = "suleymansolak"
                        });
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.UserMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Meetingid")
                        .HasColumnType("integer");

                    b.Property<int>("Userid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("created_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("updated_time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Meetingid");

                    b.HasIndex("Userid");

                    b.ToTable("user_meetings", "public");
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.Meeting", b =>
                {
                    b.HasOne("WebApplication4.Models.Entity.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.UserMeeting", b =>
                {
                    b.HasOne("WebApplication4.Models.Entity.Meeting", "Meeting")
                        .WithMany("UserMeeting")
                        .HasForeignKey("Meetingid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Entity.User", "User")
                        .WithMany("UserMeeting")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.Meeting", b =>
                {
                    b.Navigation("UserMeeting");
                });

            modelBuilder.Entity("WebApplication4.Models.Entity.User", b =>
                {
                    b.Navigation("UserMeeting");
                });
#pragma warning restore 612, 618
        }
    }
}
