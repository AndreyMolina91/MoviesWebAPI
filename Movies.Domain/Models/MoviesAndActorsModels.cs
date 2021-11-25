using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Models
{
    public class MoviesAndActorsModels
    {
        
        //Clase con propiedades de navegacion para obtener la data de Actores y peliculas
        public int ActorModelsId { get; set; }
        public int MovieModelsId { get; set; }
        public string MovieCharacter { get; set; }
        public int Order { get; set; } //Para ordenar protagonistas/Villanos/Pjs secundarios etc

        [ForeignKey("ActorModelsId")]
        public ActorModels ActorModels { get; set; }
        [ForeignKey("MovieModelsId")]
        public MovieModels MovieModels { get; set; }
    }
}
