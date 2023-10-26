using Microsoft.AspNetCore.Mvc;
using Bird_API.Models;
using Bird_API.Viewmodels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bird_API.Interfaces;

namespace Bird_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdRepo _repo;

        public BirdsController(IBirdRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var birds = await _repo.ListAllAsync();
                var birdViewModels = birds.Select(b => new BirdViewModel
                {
                    BirdID = b.BirdID,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    Habitat = b.Habitat
                }).ToList();

                return Ok(birdViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} - {ex.InnerException}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bird = await _repo.FindByIdAsync(id);

            if (bird == null)
            {
                return NotFound($"Could not find bird with id: {id}");
            }

            var birdViewModel = new BirdViewModel
            {
                BirdID = bird.BirdID,
                Name = bird.Name,
                ImageUrl = bird.ImageUrl,
                Description = bird.Description,
                Habitat = bird.Habitat
            };

            return Ok(birdViewModel);
        }

        [HttpGet("breed/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var bird = await _repo.FindByNameAsync(name);

            if (bird == null)
            {
                return NotFound($"Could not find bird with name {name}");
            }

            var birdViewModel = new BirdViewModel
            {
                BirdID = bird.BirdID,
                Name = bird.Name,
                ImageUrl = bird.ImageUrl,
                Description = bird.Description,
                Habitat = bird.Habitat
            };

            return Ok(birdViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBird(BirdViewModel birdViewModel)
        {
            try
            {
                var bird = new Bird
                {
                    Name = birdViewModel.Name,
                    ImageUrl = birdViewModel.ImageUrl,
                    Description = birdViewModel.Description,
                    Habitat = birdViewModel.Habitat
                };

                var result = await _repo.AddAsync(bird);
                var addedBirdViewModel = new BirdViewModel
                {
                    BirdID = result.BirdID,
                    Name = result.Name,
                    ImageUrl = result.ImageUrl,
                    Description = result.Description,
                    Habitat = result.Habitat
                };

                return Ok(addedBirdViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBird(int id, BirdViewModel birdViewModel)
        {
            var bird = await _repo.FindByIdAsync(id);

            if (bird != null)
            {
                bird.Name = birdViewModel.Name;
                bird.Description = birdViewModel.Description;
                bird.Habitat = birdViewModel.Habitat;
                bird.ImageUrl = birdViewModel.ImageUrl;

                await _repo.UpdateAsync(bird);

                return NoContent();
            }

            return NotFound($"Could not find bird with id {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id)
        {
            var result = await _repo.DeleteAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound($"Could not find bird with id {id}");
        }
    }
}
