using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Models
{
    public class MoviesAndMovieTheatresModels
    {
        //Entidad que contiene la relación entre peliculas y teatros
        public int MovieModelsId { get; set; }
        public int MovieTheatreModelsId { get; set; }
        [ForeignKey("MovieModelsId")]
        public MovieModels Movies { get; set; }
        [ForeignKey("MovieTheatreModelsId")]
        public MovieTheatreModels MovieTheatre { get; set; }
    }
}
