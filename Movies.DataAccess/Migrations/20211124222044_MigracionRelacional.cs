using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DataAccess.Migrations
{
    public partial class MigracionRelacional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenresModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OnCinema = table.Column<bool>(type: "bit", nullable: false),
                    MovieRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesAndActorsList",
                columns: table => new
                {
                    ActorModelsId = table.Column<int>(type: "int", nullable: false),
                    MovieModelsId = table.Column<int>(type: "int", nullable: false),
                    MovieCharacter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesAndActorsModels", x => new { x.ActorModelsId, x.MovieModelsId });
                    table.ForeignKey(
                        name: "FK_MoviesAndActorsModels_ActorModels_ActorModelsId",
                        column: x => x.ActorModelsId,
                        principalTable: "ActorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesAndActorsModels_MovieModels_MovieModelsId",
                        column: x => x.MovieModelsId,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviesAndGenreList",
                columns: table => new
                {
                    GenreModelsId = table.Column<int>(type: "int", nullable: false),
                    MovieModelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesAndGenresModels", x => new { x.GenreModelsId, x.MovieModelsId });
                    table.ForeignKey(
                        name: "FK_MoviesAndGenresModels_GenresModels_GenreModelsId",
                        column: x => x.GenreModelsId,
                        principalTable: "GenresModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesAndGenresModels_MovieModels_MovieModelsId",
                        column: x => x.MovieModelsId,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesAndActorsModels_MovieModelsId",
                table: "MoviesAndActorsList",
                column: "MovieModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesAndGenresModels_MovieModelsId",
                table: "MoviesAndGenreList",
                column: "MovieModelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesAndActorsList");

            migrationBuilder.DropTable(
                name: "MoviesAndGenreList");

            migrationBuilder.DropTable(
                name: "ActorModels");

            migrationBuilder.DropTable(
                name: "GenresModels");

            migrationBuilder.DropTable(
                name: "MovieModels");
        }
    }
}
