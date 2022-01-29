using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Service
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDBContext _dbcontext;

        public GenresService(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _dbcontext.AddAsync(genre);
            await _dbcontext.SaveChangesAsync();
            return genre;
        }

       

        public Genre Delete(Genre genre)
        {
            _dbcontext.Remove(genre);
            _dbcontext.SaveChanges();
            
            return genre;

        }

        public async Task<Genre> Get(int id)
        {
            var genre = await _dbcontext.Geners.SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var generes = await _dbcontext.Geners.OrderBy(m=>m.Id).ToListAsync();
            return generes;
        }

        public Genre Update(Genre genre)
        {
            _dbcontext.Update(genre);
            _dbcontext.SaveChanges();

            return genre;
        }

        Task IGenresService.Delete(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
