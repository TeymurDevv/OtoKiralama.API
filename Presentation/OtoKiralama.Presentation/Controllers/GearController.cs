using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearController : ControllerBase
    {
        private readonly IGearService _gearService;

        public GearController(IGearService gearService)
        {
            _gearService = gearService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllGears()
        {
            return Ok(await _gearService.GetAllGearsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGearById(int id)
        {
            return Ok(await _gearService.GetGearByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateGear(GearCreateDto gearCreateDto)
        {
            await _gearService.CreateGearAsync(gearCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGearById(int id)
        {
            await _gearService.DeleteGearAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
