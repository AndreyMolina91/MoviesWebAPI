using Movies.Domain.IRepos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models
{
    public class MovieModels : Iid
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool OnCinema { get; set; }
        public DateTime MovieRelease { get; set; }
        public string Poster { get; set; }
        public List<MoviesAndActorsModels> MoviesAndActorsModels { get; set; } //Clase con las relaciones y llaves de muchos a muchos
        public List<MoviesAndGenresModels> MoviesAndGenresModels { get; set; } //Clase con las relaciones y llaves de muchos a muchos
        public List<MoviesAndMovieTheatresModels> MoviesAndMovieTheatresModels { get; set; } //Clase con las relaciones y llaves de muchos a muchos

    }
}
