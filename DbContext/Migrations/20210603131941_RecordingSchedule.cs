using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class RecordingSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Recording",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recording_ScheduleId",
                table: "Recording",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recording_Schedule_ScheduleId",
                table: "Recording",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recording_Schedule_ScheduleId",
                table: "Recording");

            migrationBuilder.DropIndex(
                name: "IX_Recording_ScheduleId",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Recording");
        }
    }
}
