using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M133.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_Users_ErstellerrId",
                table: "Lernsets");

            migrationBuilder.RenameColumn(
                name: "ErstellerrId",
                table: "Lernsets",
                newName: "ErstellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Lernsets_ErstellerrId",
                table: "Lernsets",
                newName: "IX_Lernsets_ErstellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets",
                column: "ErstellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets");

            migrationBuilder.RenameColumn(
                name: "ErstellerId",
                table: "Lernsets",
                newName: "ErstellerrId");

            migrationBuilder.RenameIndex(
                name: "IX_Lernsets_ErstellerId",
                table: "Lernsets",
                newName: "IX_Lernsets_ErstellerrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_Users_ErstellerrId",
                table: "Lernsets",
                column: "ErstellerrId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
