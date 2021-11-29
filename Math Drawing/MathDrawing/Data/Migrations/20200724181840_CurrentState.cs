using Microsoft.EntityFrameworkCore.Migrations;

namespace MathDrawing.Data.Migrations
{
    public partial class CurrentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_AspNetUsers_PlayerUserId",
            //    table: "Players");


            //migrationBuilder.AddColumn<string>(
            //    name: "PlayerUserId",
            //    table: "Players",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "MyState",
                columns: table => new
                {
                    MyStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyPlayerID = table.Column<int>(nullable: false),
                    MyGameID = table.Column<int>(nullable: false),
                    MyCurrEq = table.Column<int>(nullable: false),
                    MyCurrScore = table.Column<int>(nullable: false),
                    MyMin = table.Column<string>(nullable: true),
                    MySec = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyState", x => x.MyStateID);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Players_PlayerUserId",
            //    table: "Players",
            //    column: "PlayerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_PlayerUserId",
                table: "Players",
                column: "PlayerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_PlayerUserId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "MyState");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerUserId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerUserId",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_CreatorId",
                table: "Players",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_CreatorId",
                table: "Players",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
