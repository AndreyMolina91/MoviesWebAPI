using System;

namespace Movies.Infraestructure.Dtos
{
    public class MovieModelDataRelationalDto
    {
        //Servirá para heredar al Dto que contiene la data relacionada entre tablas actores y moviesandactors
        //Para traer el nombre del personaje y a la vez el nombre del actor que estan en tablas diferentes
        //Esta data estará en MoviesDetailsDto
        public string Title { get; set; }
        public bool OnCinema { get; set; }
        public DateTime MovieRelease { get; set; }
        public string Poster { get; set; }

    }
}
