using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllModels(int pageNumber = 1, int pageSize = 10)
        {
            var models = await _modelService.GetAllModelsAsync(pageNumber, pageSize);
            return Ok(models);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            return Ok(await _modelService.GetModelByIdAsync(id));
        }
        [HttpGet("brand/{brandId}")]
        public async Task<IActionResult> GetAllModelsByBrandId(int brandId, int pageNumber = 1, int pageSize = 10)
        {
            var models = await _modelService.GetAllModelsByBrandId(brandId, pageNumber, pageSize);
            return Ok(models);
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateModel(ModelCreateDto modelCreateDto)
        {
            await _modelService.CreateModelAsync(modelCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelById(int id)
        {
            await _modelService.DeleteModelAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id,ModelUpdateDto modelUpdateDto)
        {
            await _modelService.UpdateAsync(id, modelUpdateDto);
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
