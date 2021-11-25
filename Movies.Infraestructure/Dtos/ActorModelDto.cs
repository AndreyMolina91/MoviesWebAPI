using Movies.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Infraestructure.Dtos
{
    public class ActorModelDto
    {    
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
        public List<MoviesAndActorsModels> MoviesAndActorsModels { get; set; }
    }
}
