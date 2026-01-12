using Microsoft.AspNetCore.Mvc;
using MMAppApi.DTO;
using MMAppApi.Interfaces;
using MMAppApi.Models;
using System.Diagnostics.Metrics;

namespace MMAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreController(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var result = await _genreRepository.GetAllAsync();

            var dtos = result.Select(genre => new GenreDto
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    Description = genre.Description
                });

                return Ok(dtos);

            //if (result.IsSuccess)
            //{
            //    var dtos = result.Value?.Select(i => new GenreDto
            //    {
            //        GenreId = i.GenreId,
            //        Name = i.Name,
            //        Description = i.Description
            //    });

            //    return Ok(dtos);
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status503ServiceUnavailable, new ProblemDetails
            //    {
            //        Status = StatusCodes.Status503ServiceUnavailable,
            //        Title = "Service Unavailable",
            //        Detail = result.ErrorMessage
            //    });
            //}
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(long id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null) return NotFound();

            var dto = new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name,
                Description = genre.Description
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var genre = new Genre
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _genreRepository.AddAsync(genre);

            await _genreRepository.SaveAsync();
            dto.GenreId = genre.GenreId;

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDto dto)
        {
            if (id != dto.GenreId)
                return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _genreRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            _genreRepository.Update(existing);

            await _genreRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var existing = await _genreRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            _genreRepository.Delete(existing);

            await _genreRepository.SaveAsync();

            return NoContent();
        }
    }
}
