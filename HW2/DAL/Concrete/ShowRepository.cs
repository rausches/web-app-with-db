using HW2.DAL.Abstract;
using HW2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW2.DAL.Concrete
{
    public class ShowRepository : Repository<Show>, IShowRepository
    {
        private readonly DbSet<Show> _shows;
        private readonly DbSet<Genre> _genres;
        private readonly DbSet<Person> _persons;
        private readonly DbSet<Credit> _credits;

        public ShowRepository(GenreAssignmentDbContext context) : base(context)
        {
            _shows = context.Shows;
            _genres = context.Genres;
            _persons = context.People;
            _credits = context.Credits;
        }

        public (int show, int movie, int tv) NumberOfShowsByType()
        {
            var showCount = _shows.Count();
            var movieCount = _shows.Count(s => s.ShowTypeId == 1);  // Assuming 1 is for movies
            var tvCount = _shows.Count(s => s.ShowTypeId == 2);    // Assuming 2 is for TV shows
            return (showCount, movieCount, tvCount);
        }

        public Show ShowWithHighestTMDBPopularity()
        {
            return _shows.OrderByDescending(s => s.TmdbPopularity).FirstOrDefault() ?? new Show();
        }

        public Show ShowWithMostIMDBVotes()
        {
            return _shows.OrderByDescending(s => s.ImdbVotes).FirstOrDefault() ?? new Show();
        }

        public IEnumerable<string> AvailableGenres() => _genres.OrderBy(g => g.GenreString).Select(g => g.GenreString).ToList();

        public IEnumerable<dynamic> DirectorWithMostShows()
        {
            var topDirector = _credits
                .Where(c => c.RoleId == 2)  // Assuming RoleId 2 is for director
                .GroupBy(c => c.PersonId)
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    DirectorName = _persons.Where(p => p.Id == g.Key).Select(p => p.FullName).FirstOrDefault() ?? "Unknown Director",
                    Shows = g.Select(c => new { Title = c.Show != null ? c.Show.Title : "Unknown Title", ReleaseYear = c.Show != null ? c.Show.ReleaseYear : 0 }).ToList()
                }).FirstOrDefault();

            return topDirector?.Shows.Select(s => new { s.Title, s.ReleaseYear, DirectorName = topDirector.DirectorName }) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<ShowDTO>> GetShowsByActorAsync(string actorName)
        {
            return await _context.Credits
                .Where(c => c.Person.FullName.Contains(actorName))
                .Select(c => new ShowDTO
                {
                    Title = c.Show.Title,
                    ReleaseYear = c.Show.ReleaseYear
                    // Map other properties if necessary
                })
                .Distinct()
                .ToListAsync();
        }
    }
}
