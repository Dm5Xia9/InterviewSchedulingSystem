using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ISSystem.DbContext.Migrations
{
    public partial class AddTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Schedule",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Calendars",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    UpdatedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Templates_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TemplateId",
                table: "Schedule",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_TemplateId",
                table: "Calendars",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatedById",
                table: "Templates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_UpdatedById",
                table: "Templates",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Templates_TemplateId",
                table: "Calendars",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Templates_TemplateId",
                table: "Schedule",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Templates_TemplateId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Templates_TemplateId",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_TemplateId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_TemplateId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Calendars");
        }
    }
}
