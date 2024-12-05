using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HW2.Controllers
{
    [ApiController]
    [Route("api/actor")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;

        public ShowsController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        [HttpGet("shows")]
        public async Task<IActionResult> GetActorShows([FromQuery] string actorName)
        {
            var shows = await _showRepository.GetShowsByActorAsync(actorName);
            if (shows == null || !shows.Any())
            {
                return NotFound("No shows found for that actor.");
            }
            return Ok(shows);
        }
    }
}
