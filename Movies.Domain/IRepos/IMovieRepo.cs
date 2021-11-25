using Movies.Domain.Models;
using System.Threading.Tasks;

namespace Movies.Domain.IRepos
{
    public interface IMovieRepo : IGeneralAsyncRepo<MovieModels>
    {
        void OrderActors(MovieModels movieModels);
        bool MovieModelsExists(int id);
    }
}
