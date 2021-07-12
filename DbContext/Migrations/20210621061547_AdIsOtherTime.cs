using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class AdIsOtherTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOtherTime",
                table: "Recording",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOtherTime",
                table: "Recording");
        }
    }
}
