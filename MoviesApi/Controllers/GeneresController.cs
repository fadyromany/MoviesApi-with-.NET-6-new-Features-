using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Controllers.DTOs;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneresController : ControllerBase
    {
        private readonly ApplicationDBContext _Context;
        public GeneresController(ApplicationDBContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var generes = await _Context.Geners.OrderBy(g=>g.Name).ToListAsync();
            return Ok(generes);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDTO dto)
        {
            var genere=new Genre { Name=dto.Name};
            await _Context.Geners.AddAsync(genere);
            _Context.SaveChanges();
            return  Ok(genere); 
        }
    }
}
