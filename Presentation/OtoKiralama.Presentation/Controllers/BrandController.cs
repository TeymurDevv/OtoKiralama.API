using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _brandService.GetAllBrandsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            return Ok(await _brandService.GetBrandByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateBrand(BrandCreateDto brandCreateDto)
        {
            await _brandService.CreateBrandAsync(brandCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryUpdateDto categoryUpdateDto)
        //{
        //    await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
        //    return StatusCode(StatusCodes.Status200OK);
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandById(int id)
        {
            await _brandService.DeleteBrandAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
