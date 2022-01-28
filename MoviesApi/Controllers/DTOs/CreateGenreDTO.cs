namespace MoviesApi.Controllers.DTOs
{
    public class CreateGenreDTO
    {
        [MaxLength(length:100)]
        public string Name { get; set; }
    }
}
