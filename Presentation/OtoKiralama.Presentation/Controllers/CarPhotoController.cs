using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.CarPhoto;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPhotoController : ControllerBase
    {
        private readonly ICarPhotoService _carPhotoService;

        public CarPhotoController(ICarPhotoService carPhotoService)
        {
            _carPhotoService = carPhotoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCarPhotosAsync(int pageNumber = 1, int pageSize = 10)
        {
            var carPhotos = await _carPhotoService.GetAllCarPhotosAsync(pageNumber, pageSize);
            return Ok(carPhotos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarPhotoByIdAsync(int id)
        {
            return Ok(await _carPhotoService.GetCarPhotoByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCarPhotoAsync(CarPhotoCreateDto carPhotoCreateDto)
        {
            await _carPhotoService.CreateCarPhotoAsync(carPhotoCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarPhotoAsync(int id)
        {
            await _carPhotoService.DeleteCarPhotoAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBody(int id, CarPhotoUpdateDto carPhotoUpdateDto)
        {
            await _carPhotoService.UpdateCarPhotoAsync(id, carPhotoUpdateDto);
            return NoContent();
        }
    }
}
