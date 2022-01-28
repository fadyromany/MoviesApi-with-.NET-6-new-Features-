namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(length: 2500)]
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }


        //EF Foreign Key
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        public static implicit operator bool(Movie? v)
        {
            throw new NotImplementedException();
        }
    }
}
