using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly IBodyService _bodyService;

        public BodyController(IBodyService bodyService)
        {
            _bodyService = bodyService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBodies(int pageNumber = 1, int pageSize = 10)
        {
            var bodies = await _bodyService.GetAllBodiesAsync(pageNumber, pageSize);
            return Ok(bodies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBodyById(int id)
        {
            return Ok(await _bodyService.GetBodyByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateBody(BodyCreateDto bodyCreateDto)
        {
            await _bodyService.CreateBodyAsync(bodyCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGearById(int id)
        {
            await _bodyService.DeleteBodyAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBody(int id, BodyUpdateDto bodyUpdateDto)
        {
            await _bodyService.UpdateBodyAsync(id, bodyUpdateDto);
            return NoContent();
        }

    }
}
