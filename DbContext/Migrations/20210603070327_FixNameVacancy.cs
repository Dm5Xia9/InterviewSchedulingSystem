using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class FixNameVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Vacancies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Vacancies",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vacancies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vacancies",
                newName: "id");
        }
    }
}
