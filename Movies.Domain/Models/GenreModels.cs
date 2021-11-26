using Movies.Domain.IRepos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models
{
    public class GenreModels : Iid
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public List<MoviesAndGenresModels> MoviesAndGenresModels { get; set; }
    }
}
