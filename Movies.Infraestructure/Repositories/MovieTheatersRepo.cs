using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;

namespace Movies.Infraestructure.Repositories
{
    public class MovieTheatersRepo : GeneralAsyncRepo<MovieTheatreModels>, IMovieTheatersRepo
    {
        public MovieTheatersRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
