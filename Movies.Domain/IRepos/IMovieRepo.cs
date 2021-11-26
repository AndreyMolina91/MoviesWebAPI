using Movies.Domain.Models;
using System.Threading.Tasks;

namespace Movies.Domain.IRepos
{
    public interface IMovieRepo : IGeneralAsyncRepo<MovieModels>
    {
        Task<MovieModels> GetIncludeThenInclude(int id);
        void OrderActors(MovieModels movieModels);
        bool MovieModelsExists(int id);
    }
}
