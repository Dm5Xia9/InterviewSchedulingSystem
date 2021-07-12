using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ISSystem.DbContext.Migrations
{
    public partial class addautolink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoLinkId",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AutoLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "text", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoLinks", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AutoLinks_AutoLinkId",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "AutoLinks");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_AutoLinkId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "AutoLinkId",
                table: "Candidates");
        }
    }
}
