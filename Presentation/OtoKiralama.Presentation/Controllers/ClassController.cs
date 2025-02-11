using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllClasses(int pageNumber = 1, int pageSize = 10)
        {
            var classes = await _classService.GetAllClassesAsync(pageNumber, pageSize);
            return Ok(classes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            return Ok(await _classService.GetClassByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateClass(ClassCreateDto classCreateDto)
        {
            await _classService.CreateClassAsync(classCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassById(int id)
        {
            await _classService.DeleteClassAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        public async Task<IActionResult> UpdateClass(int id, ClassUpdateDto classUpdateDto)
        {
            await _classService.UpdateAsync(id, classUpdateDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, ClassUpdateDto classUpdateDto)
        {
            await _classService.UpdateAsync(id, classUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
