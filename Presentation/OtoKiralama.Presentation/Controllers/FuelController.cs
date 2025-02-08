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
        public async Task<IActionResult> GetAllFuels(int pageNumber = 1, int pageSize = 10)
        {
            var fuels = await _fuelService.GetAllFuelsAsync(pageNumber, pageSize);
            return Ok(fuels);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, FuelUpdateDto fuelUpdateDto)
        {
           await _fuelService.Update(id, fuelUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
