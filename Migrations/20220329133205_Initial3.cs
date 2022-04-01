using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M133.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets");

            migrationBuilder.AlterColumn<int>(
                name: "ErstellerId",
                table: "Lernsets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets",
                column: "ErstellerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets");

            migrationBuilder.AlterColumn<int>(
                name: "ErstellerId",
                table: "Lernsets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lernsets_Users_ErstellerId",
                table: "Lernsets",
                column: "ErstellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
