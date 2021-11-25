using Movies.Domain.Models;

namespace Movies.Domain.IRepos
{
    public interface IMoviesAndActorsRepo : IGeneralAsyncRepo<MoviesAndActorsModels>
    {
        //Task Update(ActorModels actorModels);
    }
}
