using Microsoft.EntityFrameworkCore.Migrations;

namespace RunFitness.Migrations
{
    public partial class CreatePopularTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Populars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Populars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PopularDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PopularId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopularDetails_Populars_PopularId",
                        column: x => x.PopularId,
                        principalTable: "Populars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopularDetails_PopularId",
                table: "PopularDetails",
                column: "PopularId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopularDetails");

            migrationBuilder.DropTable(
                name: "Populars");
        }
    }
}
