using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IDeliveryTypeService _deliveryTypeService;

        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService)
        {
            _deliveryTypeService = deliveryTypeService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllDeliveryTypes(int pageNumber = 1, int pageSize = 10)
        {
            var deliveryTypes = await _deliveryTypeService.GetAllDeliveryTypesAsync(pageNumber, pageSize);
            return Ok(deliveryTypes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryTypeById(int id)
        {
            return Ok(await _deliveryTypeService.GetDeliveryTypeByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateDeliveryType(DeliveryTypeCreateDto deliveryTypeCreateDto)
        {
            await _deliveryTypeService.CreateDeliveryTypeAsync(deliveryTypeCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryTypeById(int id)
        {
            await _deliveryTypeService.DeleteDeliveryTypeAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryType(int? id,DeliveryTypeUpdateDto deliveryTypeUpdateDto)
        {
            await _deliveryTypeService.UpdateAsync(id, deliveryTypeUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}

