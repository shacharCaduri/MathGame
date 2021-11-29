using Microsoft.EntityFrameworkCore.Migrations;

namespace MathDrawing.Data.Migrations
{
    public partial class CNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_CreatorId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_CreatorId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_CreatorId",
                table: "Games",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
