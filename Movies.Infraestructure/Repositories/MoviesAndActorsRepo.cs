using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;

namespace Movies.Infraestructure.Repositories
{
    public class MoviesAndActorsRepo : GeneralAsyncRepo<MoviesAndActorsModels>, IMoviesAndActorsRepo
    {
        private readonly ApplicationDbContext _context;
        public MoviesAndActorsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
