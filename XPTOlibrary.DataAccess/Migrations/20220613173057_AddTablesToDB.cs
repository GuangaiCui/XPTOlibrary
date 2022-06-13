using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPTOlibrary.DataAccess.Migrations
{
    public partial class AddTablesToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atuhor",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atuhor", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cores",
                columns: table => new
                {
                    CoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cores", x => x.CoreId);
                    table.ForeignKey(
                        name: "FK_Cores_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookInformation",
                columns: table => new
                {
                    BookISBN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    Cover = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInformation", x => x.BookISBN);
                    table.ForeignKey(
                        name: "FK_BookInformation_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    BookISBN = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => new { x.BookISBN, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthor_Atuhor_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Atuhor",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthor_BookInformation_BookISBN",
                        column: x => x.BookISBN,
                        principalTable: "BookInformation",
                        principalColumn: "BookISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCores",
                columns: table => new
                {
                    BookISBN = table.Column<int>(type: "int", nullable: false),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Copies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCores", x => new { x.BookISBN, x.CoreId });
                    table.ForeignKey(
                        name: "FK_BookCores_BookInformation_BookISBN",
                        column: x => x.BookISBN,
                        principalTable: "BookInformation",
                        principalColumn: "BookISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCores_Cores_CoreId",
                        column: x => x.CoreId,
                        principalTable: "Cores",
                        principalColumn: "CoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTopic",
                columns: table => new
                {
                    BookISBN = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTopic", x => new { x.BookISBN, x.TopicId });
                    table.ForeignKey(
                        name: "FK_BookTopic_BookInformation_BookISBN",
                        column: x => x.BookISBN,
                        principalTable: "BookInformation",
                        principalColumn: "BookISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTopic_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRecord",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookISBN = table.Column<int>(type: "int", nullable: false),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    DateBorrow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRecord", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_BorrowRecord_BookInformation_BookISBN",
                        column: x => x.BookISBN,
                        principalTable: "BookInformation",
                        principalColumn: "BookISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowRecord_Cores_CoreId",
                        column: x => x.CoreId,
                        principalTable: "Cores",
                        principalColumn: "CoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowRecord_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCores_CoreId",
                table: "BookCores",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInformation_PublisherId",
                table: "BookInformation",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTopic_TopicId",
                table: "BookTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecord_BookISBN",
                table: "BorrowRecord",
                column: "BookISBN");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecord_CoreId",
                table: "BorrowRecord",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecord_UserId",
                table: "BorrowRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cores_CityId",
                table: "Cores",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "BookCores");

            migrationBuilder.DropTable(
                name: "BookTopic");

            migrationBuilder.DropTable(
                name: "BorrowRecord");

            migrationBuilder.DropTable(
                name: "Atuhor");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "BookInformation");

            migrationBuilder.DropTable(
                name: "Cores");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
