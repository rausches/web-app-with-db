using HW2.Models;
using System.Collections.Generic;

namespace HW2.DAL.Abstract
{
    public interface IShowRepository : IRepository<Show>
    {
        (int show, int movie, int tv) NumberOfShowsByType();
        Show ShowWithHighestTMDBPopularity();
        Show ShowWithMostIMDBVotes();
        IEnumerable<string> AvailableGenres();
        IEnumerable<dynamic> DirectorWithMostShows();
        Task<IEnumerable<ShowDTO>> GetShowsByActorAsync(string actorName);
        }

}
