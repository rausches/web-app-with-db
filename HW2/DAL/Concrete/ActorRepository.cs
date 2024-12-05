using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW2.DAL.Concrete
{
    public class ActorRepository : IActorRepository
    {
        private readonly GenreAssignmentDbContext _context;

        public ActorRepository(GenreAssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActorDTO>> SearchByNameAsync(string name)
        {
            return await _context.People
                .Where(p => p.FullName.Contains(name))
                .Select(p => new ActorDTO
                {
                    FullName = p.FullName,
                    JustWatchPersonId = p.JustWatchPersonId
                })
                .ToListAsync();
        }

        public async Task UpsertActorAsync(Person actor)
        {
            var existingActor = await _context.People
                .FirstOrDefaultAsync(p => p.JustWatchPersonId == actor.JustWatchPersonId);

            if (existingActor != null)
            {
                existingActor.FullName = actor.FullName;
                _context.People.Update(existingActor);
            }
            else
            {
                await _context.People.AddAsync(actor);
            }

            await _context.SaveChangesAsync();
        }
    }
}
