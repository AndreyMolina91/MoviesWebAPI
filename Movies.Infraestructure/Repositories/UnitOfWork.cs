using AutoMapper;
using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Services.AzureServices;

namespace Movies.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /*constructor herramienta para inicializar los objs de la interfaz*/

        private readonly ApplicationDbContext _context;
        public IGenreRepo Genres { get; private set; }
        public IActorRepo Actors { get; private set; }
        public IMovieRepo Movies { get; private set; }

        public IMoviesAndActorsRepo MoviesAndActors { get; private set; }

        public IMovieTheatersRepo MovieTheaters { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Genres = new GenreRepo(_context);
            Actors = new ActorRepo(_context);
            Movies = new MovieRepo(_context);
            MoviesAndActors = new MoviesAndActorsRepo(_context);
            MovieTheaters = new MovieTheatersRepo(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveData()
        {
            _context.SaveChanges();
        }
    }
}
