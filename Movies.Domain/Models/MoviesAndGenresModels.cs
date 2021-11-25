using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Models
{
    public class MoviesAndGenresModels
    {
        //Clase con propiedades de navegacion para obtener la data de Generos y peliculas
        public int GenreModelsId { get; set; }
        public int MovieModelsId { get; set; }
        [ForeignKey("GenreModelsId")]
        public GenreModels GenreModels { get; set; } //Data del genero o pelicula mediante el id
        [ForeignKey("MovieModelsId")]
        public MovieModels MovieModels { get; set; }
    }
}
