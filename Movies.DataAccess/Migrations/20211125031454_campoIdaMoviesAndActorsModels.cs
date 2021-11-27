using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DataAccess.Migrations
{
    public partial class campoIdaMoviesAndActorsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MoviesAndActorsList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MoviesAndActorsList");
        }
    }
}
