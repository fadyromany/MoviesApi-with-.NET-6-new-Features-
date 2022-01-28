using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Controllers.DTOs;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private new List<string> _allowedimagePosterExtensions = new List<string> { ".jpg",".png"};
        private long _MaxAllowedPosterSize = 1048576;

        public MoviesController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies= await _dbContext.Movies.OrderByDescending(m=>m.Rate).Include(m=>m.Genre).ToListAsync();
            return Ok(movies);
        }
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id==id);
            if (movie==null)
                return BadRequest(error: "thats movie not found");
            var dto=new MovieDetailsDTO
            {
                Title= movie.Title,
                StoryLine=movie.StoryLine,
                Year=movie.Year,
                Rate=movie.Rate,
                Poster= movie.Poster,
                GenreID=movie.GenreID,
                GenreName=movie.Genre?.Name
            };
            return Ok(movie);
        }
        [HttpGet(template: "GetByGenereID/{id}")]
        public async Task <IActionResult> GetByGenereID(int id)
        {
            var movies = await _dbContext.Movies
                .OrderByDescending(m => m.Rate)
                .Where(m=>m.GenreID==id)
                .Include(m => m.Genre)
                .ToListAsync();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDTO dto)
        {
            if (dto.Poster==null)
                return BadRequest(error:"poster is required !");
            if (!_allowedimagePosterExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest(error: "Only .Png and .Jpg Are Allowed !");
            if (dto.Poster.Length > _MaxAllowedPosterSize)
                return BadRequest(error: "the size is large than 1 MB");
            var isValidGenre = await _dbContext.Geners.AnyAsync(g=>g.Id==dto.GenreID);
            if (!isValidGenre)
                return BadRequest(error:"invalied Genere ID");    
            using var datastream=new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var movie = new Movie
            {
                GenreID = dto.GenreID,
                Title = dto.Title,
                Poster=datastream.ToArray(),
                Rate=dto.Rate,
                StoryLine=dto.StoryLine,
                Year=dto.Year
            };
            await _dbContext.AddAsync(movie);
            _dbContext.SaveChanges();
            return Ok(movie);
        }
        [HttpPut(template:"{id}")]
        public async Task <IActionResult> UpdateMovie(int id,[FromForm] MovieDTO dto)
        {
            var movie= await _dbContext.Movies.FindAsync(id);
            if (movie==null)
                return BadRequest(error: $"Not Movie Found{id}");

            var isValidGenre = await _dbContext.Geners.AnyAsync(g => g.Id == dto.GenreID);
            if (!isValidGenre)
                return BadRequest(error: "invalied Genere ID");

            if (dto.Poster!=null)
            {
                if (!_allowedimagePosterExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest(error: "Only .Png and .Jpg Are Allowed !");

                if (dto.Poster.Length > _MaxAllowedPosterSize)
                    return BadRequest(error: "the size is large than 1 MB");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster =datastream.ToArray();
            }
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.StoryLine = dto.StoryLine;
            movie.Rate = dto.Rate;  
            movie.GenreID = dto.GenreID;

            _dbContext.SaveChanges();
            return Ok(movie);
        }
        [HttpDelete(template:"{id}")]
        public async Task<IActionResult>DeleteMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
                return BadRequest(error: $"NOT FOUND WITH THIS ID {id}");

            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
