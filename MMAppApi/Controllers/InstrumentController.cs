using Microsoft.AspNetCore.Mvc;
using MMAppApi.DTO;
using MMAppApi.Helpers;
using MMAppApi.Interfaces;
using MMAppApi.Models;

namespace MMAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly IRepository<Instrument> _instrumentRepository;
        public InstrumentController(IRepository<Instrument> instrumentRepository)
        {
            _instrumentRepository = instrumentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstruments()
        {
            var result = await _instrumentRepository.GetAllAsync();

            var dtos = result.Select(i => new InstrumentDto
            {
                InstrumentId = i.InstrumentId,
                Name = i.Name,
                Description = i.Description
            });

            return Ok(dtos);

            //if (result.IsSuccess)
            //{
            //    var dtos = result.Value?.Select(i => new InstrumentDto
            //    {
            //        InstrumentId = i.InstrumentId,
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
        public async Task<IActionResult> GetInstrument(long id)
        {
            var i = await _instrumentRepository.GetByIdAsync(id);

            if (i == null) return NotFound();

            var dto = new InstrumentDto
            {
                InstrumentId = i.InstrumentId,
                Name = i.Name,
                Description = i.Description
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstrument([FromBody] InstrumentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var instrument = new Instrument
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _instrumentRepository.AddAsync(instrument);

            await _instrumentRepository.SaveAsync();
            dto.InstrumentId = instrument.InstrumentId;

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstrument(int id, [FromBody] InstrumentDto dto)
        {
            if (id != dto.InstrumentId)
                return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _instrumentRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            _instrumentRepository.Update(existing);

            await _instrumentRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrument(int id)
        {
            var existing = await _instrumentRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            _instrumentRepository.Delete(existing);

            await _instrumentRepository.SaveAsync();

            return NoContent();
        }
    }
}
