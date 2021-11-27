using System.ComponentModel.DataAnnotations;

namespace Movies.Infraestructure.Dtos
{
    public class MovieTheatreUpsertDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

    }
}
