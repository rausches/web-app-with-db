using HW2.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using HW2.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc; using Microsoft.Extensions.Logging;

namespace HW2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShowRepository _showRepository;

        public HomeController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            var (totalShows, totalMovies, totalTVShows) = _showRepository.NumberOfShowsByType();

            var infoModel = new InfoViewModel
            {
                TotalShows = totalShows,
                TotalMovies = totalMovies,
                TotalTVShows = totalTVShows,
                HighestTMDBPopularityShow = _showRepository.ShowWithHighestTMDBPopularity(),
                MostIMDBVotesShow = _showRepository.ShowWithMostIMDBVotes(),
                AvailableGenres = _showRepository.AvailableGenres(),
                TopDirectorShows = _showRepository.DirectorWithMostShows()
            };

            return View(infoModel);
        }

    }
}
