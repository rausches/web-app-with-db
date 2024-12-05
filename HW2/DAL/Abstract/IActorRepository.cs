using HW2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW2.DAL.Abstract
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDTO>> SearchByNameAsync(string name);
        Task UpsertActorAsync(Person actor);
    }
}
