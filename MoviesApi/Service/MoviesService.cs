using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Service
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDBContext _dbContext;

        public MoviesService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _dbContext.AddAsync(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = await _dbContext.Movies
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.
                Include(m => m.Genre)
                .SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public Movie Update(Movie movie)
        {
            _dbContext.Update(movie);
            _dbContext.SaveChanges();
            return movie;
        }
    }
}
