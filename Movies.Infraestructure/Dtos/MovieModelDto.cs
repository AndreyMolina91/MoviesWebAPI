using Movies.Domain.Models;
using System;
using System.Collections.Generic;

namespace Movies.Infraestructure.Dtos
{
    public class MovieModelDto
    {
        //Trae toda la data relacionada con llaves foraneas desde MoviesModels y MoviesAndActors y MoviesAndGenres
        public string Title { get; set; }
        public bool OnCinema { get; set; }
        public DateTime MovieRelease { get; set; }
        public string Poster { get; set; }
        public List<MoviesAndActorsModels> MoviesAndActorsModels { get; set; } 
        public List<MoviesAndGenresModels> MoviesAndGenresModels { get; set; } 

    }
}
