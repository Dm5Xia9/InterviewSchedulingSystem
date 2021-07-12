using Microsoft.EntityFrameworkCore.Migrations;

namespace ISSystem.DbContext.Migrations
{
    public partial class AddBaseModelWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_CreatedById",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_UpdatedById",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CreatedById",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_UpdatedById",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Candidates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Candidates",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CreatedById",
                table: "Candidates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UpdatedById",
                table: "Candidates",
                column: "UpdatedById");

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
        }
    }
}
