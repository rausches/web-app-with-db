using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HW2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IActorRepository _actorRepository;

        public AdminController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet("admin/actor/create")]
        public IActionResult Create()
        {
            return View(); // Returning the "Create" view without specifying the model initially
        }

        [HttpPost("admin/actor/create")]
        public async Task<IActionResult> Create(ActorDTO actorDto)
        {
            if (ModelState.IsValid)
            {
                var actor = new Person
                {
                    FullName = actorDto.FullName,
                    JustWatchPersonId = actorDto.JustWatchPersonId
                };

                await _actorRepository.UpsertActorAsync(actor);
                ViewBag.Message = "Actor created/updated successfully!";
                return View(actorDto); // Returning the view with the actorDto model
            }
            return View(actorDto); // Returning the view with the model, including validation errors
        }

        [HttpGet("admin/person/async")]
        public IActionResult CreateAsync()
        {
            return View("CreateAsync"); // Explicitly returning the "CreateAsync" view
        }
    }
}
