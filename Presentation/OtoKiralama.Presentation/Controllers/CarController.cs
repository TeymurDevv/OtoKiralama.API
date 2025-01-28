using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCars(int pageNumber = 1, int pageSize = 10)
        {
            var cars = await _carService.GetAllCarsAsync(pageNumber, pageSize);
            return Ok(cars);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            return Ok(await _carService.GetCarByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateCar(CarCreateDto carCreateDto)
        {
            await _carService.CreateCarAsync(carCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarById(int id)
        {
            await _carService.DeleteCarAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeCarStatus(int id)
        {
            await _carService.ChangeCarStatus(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> MarkAsDeactıve(int id)
        {
            await _carService.MarkAsDeactıve(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchCars(CarSearchDto carSearchDto)
        {
            return Ok(await _carService.GetAllFilteredCarsAsync(carSearchDto.PickupLocationId,
                carSearchDto.DropOffLocationId,
                carSearchDto.StartDate, carSearchDto.EndDate));
        }
    }
}
