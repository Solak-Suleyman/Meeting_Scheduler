using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<char>(type: "character(1)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<char>(type: "character(1)", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meetings",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    from_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    to_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetings", x => x.id);
                    table.ForeignKey(
                        name: "FK_meetings_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "public",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_meetings",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    MeetingId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_meetings_meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "public",
                        principalTable: "meetings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_meetings_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "rooms",
                columns: new[] { "Id", "created_time", "name", "status", "updated_time" },
                values: new object[] { 1, new DateTime(2023, 8, 31, 11, 40, 22, 620, DateTimeKind.Utc).AddTicks(3091), "A1", 'A', new DateTime(2023, 8, 31, 11, 40, 22, 620, DateTimeKind.Utc).AddTicks(3091) });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "created_time", "name", "password", "status", "surname", "updated_time", "user_name" },
                values: new object[] { 1, new DateTime(2023, 8, 31, 11, 40, 22, 620, DateTimeKind.Utc).AddTicks(2945), "Süleyman", "cRDtpNCeBiql5KOQsKVyrA0sAiA=", 'A', "Solak", new DateTime(2023, 8, 31, 11, 40, 22, 620, DateTimeKind.Utc).AddTicks(2949), "suleymansolak" });

            migrationBuilder.CreateIndex(
                name: "IX_meetings_RoomId",
                schema: "public",
                table: "meetings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_user_meetings_MeetingId",
                schema: "public",
                table: "user_meetings",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_user_meetings_UserId",
                schema: "public",
                table: "user_meetings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_meetings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "meetings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "public");
        }
    }
}
