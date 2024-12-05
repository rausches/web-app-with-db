using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HW2.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;

        public ActorsController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet("actor")]
        public async Task<IActionResult> SearchActor([FromQuery] string name)
        {
            var actors = await _actorRepository.SearchByNameAsync(name);
            if (actors == null || !actors.Any())
            {
                return NotFound("No actors found with that name.");
            }

            var actorResults = actors.Select(a => new ActorDTO
            {
                FullName = a.FullName,
                JustWatchPersonId = a.JustWatchPersonId
            });

            return Ok(actorResults);
        }
    }
}
