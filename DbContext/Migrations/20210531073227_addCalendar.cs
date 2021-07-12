using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ISSystem.DbContext.Migrations
{
    public partial class addCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "Vacancies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "Schedule",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CalendarId",
                table: "Vacancies",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CalendarId",
                table: "Schedule",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Calendars_CalendarId",
                table: "Schedule",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Calendars_CalendarId",
                table: "Vacancies",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Calendars_CalendarId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Calendars_CalendarId",
                table: "Vacancies");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_CalendarId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_CalendarId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Schedule");
        }
    }
}
