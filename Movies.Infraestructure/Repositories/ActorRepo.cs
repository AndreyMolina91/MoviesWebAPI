using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using System.Linq;

namespace Movies.Infraestructure.Repositories
{
    public class ActorRepo : GeneralAsyncRepo<ActorModels>, IActorRepo
    {
        private readonly ApplicationDbContext _context;
        public ActorRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        

        //public async Task Update(ActorModels actorModels)
        //{
        //    var actorModelDB = _context.ActorModels.FirstOrDefault(x => x.Id == actorModels.Id);

        //    if (actorModelDB != null)
        //    {
        //        actorModelDB.Name = actorModels.Name;
        //        actorModelDB.Birthday = actorModels.Birthday;
        //        actorModelDB.Photo = actorModels.Photo;
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
