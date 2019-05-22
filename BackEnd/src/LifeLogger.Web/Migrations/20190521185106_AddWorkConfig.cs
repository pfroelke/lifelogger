using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLogger.Web.Migrations
{
    public partial class AddWorkConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncomePerHour",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workday",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workday_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workday_UserId",
                table: "Workday",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workday");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IncomePerHour",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Users");
        }
    }
}
