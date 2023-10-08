using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bird_API.Data;
using Bird_API.Models;

namespace Bird_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly BirdContext _context;

        public BirdsController(BirdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Birds.ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Birds.FindAsync(id);
            return Ok(result);
        }
        [HttpGet("breed/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _context.Birds.FindAsync(name);
            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> AddBird(Bird bird)
        {
            var result = await _context.Birds.AddAsync(bird);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBird(int id,Bird bird)
        {
            var result = await _context.Birds.FindAsync(id);
            result.Name = bird.Name;
            result.Description = bird.Description;
            result.Habitat = bird.Habitat;
            result.ImageUrl = bird.ImageUrl;
            _context.Birds.Update(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id, Bird bird)
        {
            var result = await _context.Birds.FindAsync(id);
            _context.Birds.Remove(result);
            return NoContent();
        }



    }
}
