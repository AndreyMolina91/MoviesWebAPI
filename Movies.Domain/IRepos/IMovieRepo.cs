using Movies.Domain.Models;
using System.Threading.Tasks;

namespace Movies.Domain.IRepos
{
    public interface IMovieRepo : IGeneralAsyncRepo<MovieModels>
    {
        Task<MovieModels> UpdateMovie(MovieModels movieModels);
        void OrderActors(MovieModels movieModels);
        bool MovieModelsExists(int id);
    }
}
