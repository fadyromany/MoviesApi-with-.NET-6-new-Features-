
namespace MoviesApi.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(length:100)]
        public string Name { get; set; }

    }
}
