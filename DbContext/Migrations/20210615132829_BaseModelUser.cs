using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class BaseModelUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStopped",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vacancies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Schedule",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recording",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Calendars",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_UserId",
                table: "Vacancies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recording_UserId",
                table: "Recording",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserId",
                table: "Candidates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_AspNetUsers_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_UserId",
                table: "Candidates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recording_AspNetUsers_UserId",
                table: "Recording",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_AspNetUsers_UserId",
                table: "Schedule",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_UserId",
                table: "Vacancies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_AspNetUsers_UserId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_UserId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Recording_AspNetUsers_UserId",
                table: "Recording");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_AspNetUsers_UserId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_UserId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_UserId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Recording_UserId",
                table: "Recording");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_UserId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Calendars");

            migrationBuilder.AddColumn<bool>(
                name: "IsStopped",
                table: "Vacancies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
