using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPTOlibrary.DataAccess.Migrations
{
    public partial class borrowrecordaddapplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BorrowRecord",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecord_ApplicationUserId",
                table: "BorrowRecord",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRecord_AspNetUsers_ApplicationUserId",
                table: "BorrowRecord",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRecord_AspNetUsers_ApplicationUserId",
                table: "BorrowRecord");

            migrationBuilder.DropIndex(
                name: "IX_BorrowRecord_ApplicationUserId",
                table: "BorrowRecord");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BorrowRecord");
        }
    }
}
