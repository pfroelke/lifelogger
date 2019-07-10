using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLogger.Web.Migrations
{
    public partial class AddWorkdays11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Users_UserId",
                table: "Workdays");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Workdays",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Workdays_UserId",
                table: "Workdays",
                newName: "IX_Workdays_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Users_OwnerId",
                table: "Workdays",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workdays_Users_OwnerId",
                table: "Workdays");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Workdays",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workdays_OwnerId",
                table: "Workdays",
                newName: "IX_Workdays_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workdays_Users_UserId",
                table: "Workdays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
