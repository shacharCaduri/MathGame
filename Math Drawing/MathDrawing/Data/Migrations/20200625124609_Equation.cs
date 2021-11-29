using Microsoft.EntityFrameworkCore.Migrations;

namespace MathDrawing.Data.Migrations
{
    public partial class Equation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equations",
                columns: table => new
                {
                    EquationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquationQ = table.Column<string>(nullable: true),
                    Result = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equations", x => x.EquationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equations");
        }
    }
}
