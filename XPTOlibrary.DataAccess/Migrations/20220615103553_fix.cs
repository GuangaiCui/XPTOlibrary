using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPTOlibrary.DataAccess.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInformation_Atuhor_AuthorId",
                table: "BookInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atuhor",
                table: "Atuhor");

            migrationBuilder.RenameTable(
                name: "Atuhor",
                newName: "Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInformation_Author_AuthorId",
                table: "BookInformation",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInformation_Author_AuthorId",
                table: "BookInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Atuhor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atuhor",
                table: "Atuhor",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInformation_Atuhor_AuthorId",
                table: "BookInformation",
                column: "AuthorId",
                principalTable: "Atuhor",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
