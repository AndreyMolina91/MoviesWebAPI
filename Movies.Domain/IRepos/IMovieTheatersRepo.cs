using Movies.Domain.Models;

namespace Movies.Domain.IRepos
{
    public interface IMovieTheatersRepo : IGeneralAsyncRepo<MovieTheatreModels>
    {
        //Task Update(ActorModels actorModels);
    }
}
