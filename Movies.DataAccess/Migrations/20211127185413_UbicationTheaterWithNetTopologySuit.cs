using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Movies.DataAccess.Migrations
{
    public partial class UbicationTheaterWithNetTopologySuit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Ubication",
                table: "MovieTheatreModels",
                type: "geography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubication",
                table: "MovieTheatreModels");
        }
    }
}
