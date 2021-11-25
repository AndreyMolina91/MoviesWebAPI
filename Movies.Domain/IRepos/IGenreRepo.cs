using Movies.Domain.Models;
using System.Threading.Tasks;

namespace Movies.Domain.IRepos
{
    public interface IGenreRepo : IGeneralAsyncRepo<GenreModels>
    {
        Task Update(GenreModels genreModels);
    }  
}
