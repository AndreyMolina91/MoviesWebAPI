using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models
{
    public class ActorModels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        //Url de la imagen
        public string Photo { get; set; }
        //Propiedad de navegacion hacia ...
        public List<MoviesAndActorsModels> MoviesAndActorsModels { get; set; }
    }
}
