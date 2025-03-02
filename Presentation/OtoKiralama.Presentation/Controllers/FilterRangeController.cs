using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.FilterRange;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterRangeController : ControllerBase
    {
        private readonly IFilterRangeService _filterRangeService;

        public FilterRangeController(IFilterRangeService filterRangeService)
        {
            _filterRangeService = filterRangeService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllFilterRanges(int pageNumber = 1, int pageSize = 10)
        {
            var filterRanges = await _filterRangeService.GetAllFilterRangeAsync(pageNumber, pageSize);
            return Ok(filterRanges);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilterRangeById(int id)
        {
            return Ok(await _filterRangeService.GetFilterRangeByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateFilterRange(FilterRangeCreateDto filterRangeCreateDto)
        {
            await _filterRangeService.CreateFilterRangeAsync(filterRangeCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilterRangeById(int id)
        {
            await _filterRangeService.DeleteFilterRangeAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilterRange(int id, FilterRangeUpdateDto filterRangeUpdateDto)
        {
            await _filterRangeService.UpdateFilterRangeAsync(id, filterRangeUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}

