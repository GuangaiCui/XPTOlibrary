using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPTOlibrary.DataAccess.Migrations
{
    public partial class birthdaybecameregistertime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "AspNetUsers",
                newName: "RegisterTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterTime",
                table: "AspNetUsers",
                newName: "Birthday");
        }
    }
}
