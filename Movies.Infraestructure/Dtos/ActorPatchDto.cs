
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Infraestructure.Dtos
{
    public class ActorPatchDto
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
