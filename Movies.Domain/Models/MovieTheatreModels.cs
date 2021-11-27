using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models
{
    public class MovieTheatreModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        //Listado con prop de navegación hacia la entidad con relaciones
        public List<MoviesAndMovieTheatresModels> MoviesAndMovieTheatresList { get; set; }
    }
}
