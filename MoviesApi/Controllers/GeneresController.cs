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
        [HttpPut (template:"{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody]CreateGenreDTO dto)
        {
            var genere=await _Context.Geners.SingleOrDefaultAsync(g=>g.Id==id);
            if(genere==null)
                return NotFound();
            genere.Name=dto.Name;
            _Context.SaveChanges();
            return Ok(genere);
        }
        [HttpDelete (template:"{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genere = await _Context.Geners.SingleOrDefaultAsync(g => g.Id == id);
            if (genere == null)
                return NotFound();
            _Context.Geners.Remove(genere);
            _Context.SaveChanges();
            return Ok();

        }
    }
}
