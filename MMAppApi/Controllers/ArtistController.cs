using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMAppApi.DTO;
using MMAppApi.Helpers;
using MMAppApi.Interfaces;
using MMAppApi.Models;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace MMAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly MmappContext _context;
        private readonly IRepository<Artist> _artistRepository;

        public ArtistController(MmappContext context, IRepository<Artist> artistRepository)
        {
            _context = context;
            _artistRepository = artistRepository;
        }

        public async Task<IActionResult> GetArtists()
        {
            var result = await _artistRepository.GetAllAsync();

            var dtos = result.Select(i => new ArtistDto
            {
                ArtistId = i.ArtistId,
                Name = i.Name,
                Description = i.Description
            });

            return Ok(dtos);

            //if (result.IsSuccess)
            //{
            //    var dtos = result.Value?.Select(i => new ArtistDto
            //    {
            //        ArtistId = i.ArtistId,
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateArtistRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Artist artist = ArtistHelper.AddArtist(_context, request);

            await _artistRepository.AddAsync(artist);

            await _artistRepository.SaveAsync();

            ArtistDto artistDto = new ArtistDto
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name,
                Description = artist.Description
            };

            return Ok(artistDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(long id)
        {
            //var artist = _context.Artists.Include(i => i.Instruments).Include(g => g.Genres).Where(i => i.Id == id).Single();

            var artist = await _artistRepository.GetByIdAsyncInclude(filter: a => a.ArtistId == id, includeProperties: new Expression<Func<Artist, object>>[]
            {
                g => g.Genres,
                g => g.Instruments
            });

            if (artist == null) return NotFound();

            var dto = new ArtistIncludeDto
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name,
                Description = artist.Description,
                Genres = new List<GenreDto>(),
                Instruments = new List<InstrumentDto>()
            };

            foreach (var genre in artist.Genres)
            {
                dto.Genres.Add(new GenreDto
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    Description = genre.Description
                });
            }

            foreach (var instrument in artist.Instruments)
            {
                dto.Instruments.Add(new InstrumentDto
                {
                    InstrumentId = instrument.InstrumentId,
                    Name = instrument.Name,
                    Description = instrument.Description
                });
            }

            return Ok(dto);
        }
    }
}
