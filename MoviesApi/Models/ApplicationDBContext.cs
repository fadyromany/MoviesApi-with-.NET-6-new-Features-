using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<Genre> Geners { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options)
        {
        }

    }
}
