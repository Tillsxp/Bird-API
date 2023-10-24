using Microsoft.AspNetCore.Mvc;
using Bird_API.Models;
using Bird_API.Interfaces;

namespace Bird_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdRepo _repo;

        public BirdsController( IBirdRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await _repo.ListAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.FindByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("breed/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _repo.FindByNameAsync(name);
            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> AddBird(Bird bird)
        {
            var result = await _repo.AddAsync(bird);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBird(int id,Bird bird)
        {
            var result = await _repo.FindByIdAsync(id);
            result.Name = bird.Name;
            result.Description = bird.Description;
            result.Habitat = bird.Habitat;
            result.ImageUrl = bird.ImageUrl;
            await _repo.UpdateAsync(result);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id)
        {
            var result = await _repo.DeleteAsync(id);
            return NoContent();
        }



    }
}
