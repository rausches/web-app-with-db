using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HW2.Controllers
{
    [ApiController]
    [Route("admin/person/async")]
    public class ActorApiController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;

        public ActorApiController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpPut]
        public async Task<IActionResult> UpsertActor([FromBody] ActorDTO actorDto)
        {
            if (actorDto == null || string.IsNullOrEmpty(actorDto.FullName) || actorDto.JustWatchPersonId <= 0)
            {
                return BadRequest("Actor's name and valid JustWatchPersonId are required.");
            }

            var actor = new Person
            {
                FullName = actorDto.FullName,
                JustWatchPersonId = actorDto.JustWatchPersonId
            };

            await _actorRepository.UpsertActorAsync(actor);
            return Ok("Actor upserted successfully.");
        }
    }
}