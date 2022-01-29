namespace MoviesApi.Service
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Task Delete(Genre genre);
        Task<Genre> Get(int id);

    }
}
