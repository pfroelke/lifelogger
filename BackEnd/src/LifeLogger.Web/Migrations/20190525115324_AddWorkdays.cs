using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLogger.Web.Migrations
{
    public partial class AddWorkdays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workday_Users_UserId",
                table: "Workday");

            migrationBuilder.DropTable(
                name: "LoggedItem");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workday",
                table: "Workday");

            migrationBuilder.RenameTable(
                name: "Workday",
                newName: "Workdays");

            migrationBuilder.RenameIndex(
                name: "IX_Workday_UserId",
                table: "Workdays",
                newName: "IX_Workdays_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workdays",
                table: "Workdays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Users_UserId",
                table: "Workdays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Users_UserId",
                table: "Workdays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workdays",
                table: "Workdays");

            migrationBuilder.RenameTable(
                name: "Workdays",
                newName: "Workday");

            migrationBuilder.RenameIndex(
                name: "IX_Workdays_UserId",
                table: "Workday",
                newName: "IX_Workday_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workday",
                table: "Workday",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "LoggedItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggedItem_Tag_TagName",
                        column: x => x.TagName,
                        principalTable: "Tag",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggedItem_TagName",
                table: "LoggedItem",
                column: "TagName");

            migrationBuilder.AddForeignKey(
                name: "FK_Workday_Users_UserId",
                table: "Workday",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
