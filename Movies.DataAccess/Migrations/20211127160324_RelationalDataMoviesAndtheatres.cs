using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DataAccess.Migrations
{
    public partial class RelationalDataMoviesAndtheatres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieTheatreModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTheatreModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesAndMovieTheatresModels",
                columns: table => new
                {
                    MovieModelsId = table.Column<int>(type: "int", nullable: false),
                    MovieTheatreModelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesAndMovieTheatresModels", x => new { x.MovieModelsId, x.MovieTheatreModelsId });
                    table.ForeignKey(
                        name: "FK_MoviesAndMovieTheatresModels_MovieModels_MovieModelsId",
                        column: x => x.MovieModelsId,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesAndMovieTheatresModels_MovieTheatreModels_MovieTheatreModelsId",
                        column: x => x.MovieTheatreModelsId,
                        principalTable: "MovieTheatreModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesAndMovieTheatresModels_MovieTheatreModelsId",
                table: "MoviesAndMovieTheatresModels",
                column: "MovieTheatreModelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesAndMovieTheatresModels");

            migrationBuilder.DropTable(
                name: "MovieTheatreModels");
        }
    }
}
