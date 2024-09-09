using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Services;

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
        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDto locationCreateDto)
        {
            await _locationService.CreateLocationAsync(locationCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationById(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
