using Movies.Domain.Models;

namespace Movies.Domain.IRepos
{
    public interface IMovieRepo : IGeneralAsyncRepo<MovieModels>
    {
        void OrderActors(MovieModels movieModels);
        bool MovieModelsExists(int id);
    }
}
