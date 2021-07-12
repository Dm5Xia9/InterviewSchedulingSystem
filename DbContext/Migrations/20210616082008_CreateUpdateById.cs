using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class CreateUpdateById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vacancies",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_UserId",
                table: "Vacancies",
                newName: "IX_Vacancies_UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Schedule",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule",
                newName: "IX_Schedule_UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Recording",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Recording_UserId",
                table: "Recording",
                newName: "IX_Recording_UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Candidates",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_UserId",
                table: "Candidates",
                newName: "IX_Candidates_UpdatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Calendars",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars",
                newName: "IX_Calendars_UpdatedById");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Vacancies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Schedule",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Recording",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Calendars",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CreatedById",
                table: "Vacancies",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CreatedById",
                table: "Schedule",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recording_CreatedById",
                table: "Recording",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CreatedById",
                table: "Candidates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_CreatedById",
                table: "Calendars",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_AspNetUsers_CreatedById",
                table: "Calendars",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_AspNetUsers_UpdatedById",
                table: "Calendars",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_CreatedById",
                table: "Candidates",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_UpdatedById",
                table: "Candidates",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recording_AspNetUsers_CreatedById",
                table: "Recording",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recording_AspNetUsers_UpdatedById",
                table: "Recording",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_AspNetUsers_CreatedById",
                table: "Schedule",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_AspNetUsers_UpdatedById",
                table: "Schedule",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_CreatedById",
                table: "Vacancies",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_UpdatedById",
                table: "Vacancies",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_AspNetUsers_CreatedById",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_AspNetUsers_UpdatedById",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_CreatedById",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_UpdatedById",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Recording_AspNetUsers_CreatedById",
                table: "Recording");

            migrationBuilder.DropForeignKey(
                name: "FK_Recording_AspNetUsers_UpdatedById",
                table: "Recording");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_AspNetUsers_CreatedById",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_AspNetUsers_UpdatedById",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_CreatedById",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_UpdatedById",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_CreatedById",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_CreatedById",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Recording_CreatedById",
                table: "Recording");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CreatedById",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_CreatedById",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Recording");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Calendars");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Vacancies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_UpdatedById",
                table: "Vacancies",
                newName: "IX_Vacancies_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Schedule",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_UpdatedById",
                table: "Schedule",
                newName: "IX_Schedule_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Recording",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recording_UpdatedById",
                table: "Recording",
                newName: "IX_Recording_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Candidates",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_UpdatedById",
                table: "Candidates",
                newName: "IX_Candidates_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Calendars",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Calendars_UpdatedById",
                table: "Calendars",
                newName: "IX_Calendars_UserId");

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
    }
}
