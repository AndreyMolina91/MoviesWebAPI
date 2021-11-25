using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infraestructure.Repositories
{
    public class MovieRepo : GeneralAsyncRepo<MovieModels>, IMovieRepo
    {
        private readonly ApplicationDbContext _context;
        public MovieRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MovieModels> UpdateMovie(MovieModels movieModels)
        {
            MovieModels movieModelDB = await _context.MovieModels.FirstOrDefaultAsync(x=>x.Title == movieModels.Title);
            if (movieModelDB!=null)
            {
                movieModelDB.Title = movieModels.Title;
                movieModelDB.OnCinema = movieModels.OnCinema;
                movieModelDB.MovieRelease = movieModels.MovieRelease;
                movieModelDB.Poster = movieModels.Poster;
                movieModelDB.MoviesAndGenresModels = movieModels.MoviesAndGenresModels;
                movieModelDB.MoviesAndActorsModels = movieModels.MoviesAndActorsModels;

            }
            return movieModelDB;
        }

        //Metodo para asignar el orden a los actores al insertarlos en la base de datos
        public void OrderActors(MovieModels movieModels)
        {
            //Recibimos una pelicula por parametro
            if (movieModels.MoviesAndActorsModels != null)
            {
                //Si pelicula y actores contiene un listado de actores entonces les asignamos un orden numerado 
                //Este orden es incremental en i++
                for (int i = 0; i < movieModels.MoviesAndActorsModels.Count; i++)
                {
                    //Actores ordenados segun el cliente los ingresa, ejemplo 0 Tom, 1 Samuel, 2 Jack...etc
                    //En este orden se mostrará siempre los actors de la pelicula
                    movieModels.MoviesAndActorsModels[i].Order = i;
                }
            }
        }
        public bool MovieModelsExists(int id)
        {
            return _context.MovieModels.Any(e => e.Id == id);
        }
    }
}
