using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class addHiddenVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Vacancies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Vacancies");
        }
    }
}
