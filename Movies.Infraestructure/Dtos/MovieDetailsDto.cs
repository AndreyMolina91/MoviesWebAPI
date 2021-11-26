using System.Collections.Generic;

namespace Movies.Infraestructure.Dtos
{
    public class MovieDetailsDto : MovieModelDataRelationalDto
    {
        //Contiene la data relacionada entre las tablas
        public List<GenreDetailsDto> GenresDetails { get; set; }
        public List<ActorDetailsDto> ActorDetails { get; set; }

    }
}
