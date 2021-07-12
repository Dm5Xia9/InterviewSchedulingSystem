using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ISSystem.DbContext.Migrations
{
    public partial class editAutoLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AutoLinks_AutoLinkId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_AutoLinkId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AutoLinks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_AutoLinks_Candidates_Id",
                table: "AutoLinks",
                column: "Id",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoLinks_Candidates_Id",
                table: "AutoLinks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AutoLinks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_AutoLinkId",
                table: "Candidates",
                column: "AutoLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AutoLinks_AutoLinkId",
                table: "Candidates",
                column: "AutoLinkId",
                principalTable: "AutoLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
