using Movies.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Domain.IRepos
{
    public interface IActorRepo : IGeneralAsyncRepo<ActorModels>
    {
        //Task Update(ActorModels actorModels);
    }
}
