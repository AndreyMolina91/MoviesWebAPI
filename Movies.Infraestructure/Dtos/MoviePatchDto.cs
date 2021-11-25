using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Infraestructure.Dtos
{
    public class MoviePatchDto
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool OnCinema { get; set; }
        public DateTime MovieRelease { get; set; }
    }
}
