using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class RecordingVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Recording",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recording_VacancyId",
                table: "Recording",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recording_Vacancies_VacancyId",
                table: "Recording",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recording_Vacancies_VacancyId",
                table: "Recording");

            migrationBuilder.DropIndex(
                name: "IX_Recording_VacancyId",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Recording");
        }
    }
}
