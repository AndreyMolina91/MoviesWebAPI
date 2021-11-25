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

            base.OnModelCreating(modelBuilder);//uso de Identity db context
        }

        //DbSets para modelos sin relaciones
        public DbSet<GenreModels> GenresModels { get; set; }
        public DbSet<ActorModels> ActorModels { get; set; }
        public DbSet<MovieModels> MovieModels { get; set; }

        //DbSet para clases de relacion muchos a muchos
        public DbSet<MoviesAndGenresModels> MoviesAndGenresModels { get; set; }
        public DbSet<MoviesAndActorsModels> MoviesAndActorsModels { get; set; }
    }
}
