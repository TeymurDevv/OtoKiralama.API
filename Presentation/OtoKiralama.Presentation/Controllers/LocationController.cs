using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Interfaces;
namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _locationService.GetAllLocationsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            return Ok(await _locationService.GetLocationByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDto locationCreateDto)
        {
            await _locationService.CreateLocationAsync(locationCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationById(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
