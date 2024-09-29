using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelController : ControllerBase
    {
        private readonly IFuelService _fuelService;

        public FuelController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllFuels()
        {
            return Ok(await _fuelService.GetAllFuelsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuelById(int id)
        {
            return Ok(await _fuelService.GetFuelByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateFuel(FuelCreateDto fuelCreateDto)
        {
            await _fuelService.CreateFuelAsync(fuelCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelById(int id)
        {
            await _fuelService.DeleteFuelAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
