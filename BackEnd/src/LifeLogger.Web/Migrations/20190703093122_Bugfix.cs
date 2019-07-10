using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLogger.Web.Migrations
{
    public partial class Bugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "IncMade",
                table: "Workdays",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "IncPerHour",
                table: "Workdays",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WorkTime",
                table: "Workdays",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncMade",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "IncPerHour",
                table: "Workdays");

            migrationBuilder.DropColumn(
                name: "WorkTime",
                table: "Workdays");
        }
    }
}
