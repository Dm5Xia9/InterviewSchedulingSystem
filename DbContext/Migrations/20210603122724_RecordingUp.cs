using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ISSystem.Models;

namespace ISSystem.DbContext.Migrations
{
    public partial class RecordingUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSchedule",
                table: "Recording");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Recording",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Recording");

            migrationBuilder.AddColumn<TimeSchedule>(
                name: "TimeSchedule",
                table: "Recording",
                type: "jsonb",
                nullable: true);
        }
    }
}
