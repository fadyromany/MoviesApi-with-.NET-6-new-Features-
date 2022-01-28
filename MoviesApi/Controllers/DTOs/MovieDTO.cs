namespace MoviesApi.Controllers.DTOs
{
    public class MovieDTO
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(length: 2500)]
        public string StoryLine { get; set; }
        public IFormFile? Poster { get; set; }
        public int GenreID { get; set; }
    }
}
