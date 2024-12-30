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
        public async Task<IActionResult> GetAllLocations(int pageNumber = 1, int pageSize = 10)
        {
            var locations = await _locationService.GetAllLocationsAsync(pageNumber, pageSize);
            return Ok(locations);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            return Ok(await _locationService.GetLocationByIdAsync(id));
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetLocationsByKeyword(string name,int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _locationService.GetAllLocationsByNameAsync(name, pageNumber, pageSize));
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
