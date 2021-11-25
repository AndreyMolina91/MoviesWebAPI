using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infraestructure.Repositories
{
    public class GenreRepo : GeneralAsyncRepo<GenreModels>, IGenreRepo
    {
        private readonly ApplicationDbContext _context;
        public GenreRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(GenreModels genreModels)
        {
            var genreModelDB = _context.GenresModels.FirstOrDefault(x=>x.Id == genreModels.Id);
            if (genreModelDB!=null)
            {
                genreModelDB.Title = genreModels.Title;
                await _context.SaveChangesAsync();
            }
        }
    }
}
