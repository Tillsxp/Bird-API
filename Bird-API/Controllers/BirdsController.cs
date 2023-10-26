﻿using Microsoft.AspNetCore.Mvc;
using Bird_API.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

            try
            {
                

            var result = await _repo.ListAllAsync();
            return Ok(result);


            }
            catch (Exception ex)
            {

                return StatusCode(500, $"{ex.Message} - {ex.InnerException}");
            }

            

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _repo.FindByIdAsync(id);


            if(result == null)
            {
                return NotFound($"Could not find bird with id: {id}");
            }


            return Ok(result);
        }
        [HttpGet("breed/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {

            var result = await _repo.FindByNameAsync(name);

            if(result == null)
            {
                return NotFound($"Could not find bird with name {name}");
            }


            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> AddBird(Bird bird)
        {

            try
            {

            var result = await _repo.AddAsync(bird);
            return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException}");
                throw;
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBird(int id,Bird bird)
        {
            var result = await _repo.FindByIdAsync(id);

            if(result != null)
            {
            result.Name = bird.Name;
            result.Description = bird.Description;
            result.Habitat = bird.Habitat;
            result.ImageUrl = bird.ImageUrl;
            await _repo.UpdateAsync(result);
            return NoContent();
 
            }
            return NotFound($"Could not find bird with id {id}");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id)
        {
            var result = await _repo.DeleteAsync(id);

            if(result != false)
            {

                return NoContent();
            }
            return NotFound($"Could not find bird with id {id}");

        }



    }
}
