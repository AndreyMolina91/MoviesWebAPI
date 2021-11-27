using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //APIFluente para configurar llaves primarias compuestas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesAndActorsModels>()
                .HasKey(x => new { x.ActorModelsId, x.MovieModelsId }); //Composicion de actorid y peliculaid para una llave primaria

            modelBuilder.Entity<MoviesAndGenresModels>()
               .HasKey(x => new { x.GenreModelsId, x.MovieModelsId });

            modelBuilder.Entity<MoviesAndMovieTheatresModels>()
                .HasKey(x => new { x.MovieModelsId, x.MovieTheatreModelsId });

            base.OnModelCreating(modelBuilder);//uso de Identity db context
        }

        //DbSets para modelos sin relaciones
        //Entidad generos
        public DbSet<GenreModels> GenresModels { get; set; }
        //Entidad actores
        public DbSet<ActorModels> ActorModels { get; set; }
        //Entidad peliculas
        public DbSet<MovieModels> MovieModels { get; set; }

        //Relacion entre peliculas y generos
        public DbSet<MoviesAndGenresModels> MoviesAndGenresModels { get; set; }
        //Relacion entre peliculas y actores
        public DbSet<MoviesAndActorsModels> MoviesAndActorsModels { get; set; }
        //Entidad teatros
        public DbSet<MovieTheatreModels> MovieTheatreModels { get; set; }
        //Relacion entre peliculas y teatros
        public DbSet<MoviesAndMovieTheatresModels> MoviesAndMovieTheatresModels { get; set; }
    }
}
