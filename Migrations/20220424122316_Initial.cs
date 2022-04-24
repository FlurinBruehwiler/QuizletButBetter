using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M133.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "StudySets",
                columns: table => new
                {
                    StudySetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySets", x => x.StudySetId);
                    table.ForeignKey(
                        name: "FK_StudySets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudySetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "StudySetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learns",
                columns: table => new
                {
                    LearnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StudySetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learns", x => x.LearnId);
                    table.ForeignKey(
                        name: "FK_Learns_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "StudySetId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Learns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LearnCards",
                columns: table => new
                {
                    LearnCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Pool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepeatedFalse = table.Column<int>(type: "int", nullable: false),
                    LastCardIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnCards", x => x.LearnCardId);
                    table.ForeignKey(
                        name: "FK_LearnCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnCards_Learns_LearnId",
                        column: x => x.LearnId,
                        principalTable: "Learns",
                        principalColumn: "LearnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_StudySetId",
                table: "Cards",
                column: "StudySetId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnCards_CardId",
                table: "LearnCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnCards_LearnId",
                table: "LearnCards",
                column: "LearnId");

            migrationBuilder.CreateIndex(
                name: "IX_Learns_StudySetId",
                table: "Learns",
                column: "StudySetId");

            migrationBuilder.CreateIndex(
                name: "IX_Learns_UserId",
                table: "Learns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySets_UserId",
                table: "StudySets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnCards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Learns");

            migrationBuilder.DropTable(
                name: "StudySets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
