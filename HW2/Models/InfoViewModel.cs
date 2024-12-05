namespace HW2.Models
{
    public class InfoViewModel
    {
        public int TotalShows { get; set; }
        public int TotalMovies { get; set; }
        public int TotalTVShows { get; set; }
        public Show HighestTMDBPopularityShow { get; set; } = new Show();
        public Show MostIMDBVotesShow { get; set; } = new Show();
        public IEnumerable<string> AvailableGenres { get; set; } = new List<string>();
        public IEnumerable<dynamic> TopDirectorShows { get; set; } = new List<dynamic>();

    }
}
