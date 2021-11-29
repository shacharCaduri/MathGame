using Microsoft.EntityFrameworkCore.Migrations;

namespace MathDrawing.Data.Migrations
{
    public partial class CurrentState2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserMailId",
                table: "MyState",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyState_UserMailId",
                table: "MyState",
                column: "UserMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyState_AspNetUsers_UserMailId",
                table: "MyState",
                column: "UserMailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyState_AspNetUsers_UserMailId",
                table: "MyState");

            migrationBuilder.DropIndex(
                name: "IX_MyState_UserMailId",
                table: "MyState");

            migrationBuilder.DropColumn(
                name: "UserMailId",
                table: "MyState");
        }
    }
}
