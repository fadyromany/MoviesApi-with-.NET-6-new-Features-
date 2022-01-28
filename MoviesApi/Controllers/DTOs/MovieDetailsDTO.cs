namespace MoviesApi.Controllers.DTOs
{
    public class MovieDetailsDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }
        public int GenreID { get; set; }
        public String GenreName { get; set; }
    }
}
