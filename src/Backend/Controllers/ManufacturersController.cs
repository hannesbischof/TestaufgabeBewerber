using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Backend.Models.DTOs;
using Backend.Models.Domain;
using Backend.Mediator;
using Backend.Features.Manufacturers.Requests;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManufacturersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerDto>>> GetManufacturers(int pageNumber = 1, int pageSize = 10, string? sortBy = null, string? sortOrder = null, string? filter = null)
        {
            var request = new GetManufacturersRequest(pageNumber, pageSize, sortBy, sortOrder, filter);
            var manufacturers = await _mediator.Send(request);
            var manufacturerDtos = _mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers);
            return Ok(manufacturerDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerDto>> GetManufacturerById(int id)
        {
            var request = new GetManufacturerByIdRequest(id);
            var manufacturer = await _mediator.Send(request);
            if (manufacturer == null)
                return NotFound();
            var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);
            return Ok(manufacturerDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ManufacturerDto>> AddManufacturer([FromBody] ManufacturerDto manufacturerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var domainManufacturer = _mapper.Map<DomainManufacturer>(manufacturerDto);
                var createdManufacturer = await _mediator.Send(new AddManufacturerRequest(domainManufacturer));
                var createdDto = _mapper.Map<ManufacturerDto>(createdManufacturer);
                return CreatedAtAction(nameof(GetManufacturerById), new { id = createdDto.Id }, createdDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ManufacturerDto>> UpdateManufacturer(int id, [FromBody] ManufacturerDto manufacturerDto)
        {
            if (id != manufacturerDto.Id)
                return BadRequest(new { message = "Manufacturer ID mismatch." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var domainManufacturer = _mapper.Map<DomainManufacturer>(manufacturerDto);
                var updatedManufacturer = await _mediator.Send(new UpdateManufacturerRequest(domainManufacturer));
                var updatedDto = _mapper.Map<ManufacturerDto>(updatedManufacturer);
                return Ok(updatedDto);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await _mediator.Send(new GetManufacturerByIdRequest(id));
            if (manufacturer == null)
                return NotFound();

            await _mediator.Send(new DeleteManufacturerRequest(id));
            return NoContent();
        }
    }
}