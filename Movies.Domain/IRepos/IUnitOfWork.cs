using System;

namespace Movies.Domain.IRepos
{
    public interface IUnitOfWork : IDisposable
    {
        //Acá crearemos los objs de tipo interface de los modelos
        IGenreRepo Genres { get; }
        IActorRepo Actors { get; }
        IMovieRepo Movies { get; }
        IMoviesAndActorsRepo MoviesAndActors { get; }
        void SaveData();
    }
}
