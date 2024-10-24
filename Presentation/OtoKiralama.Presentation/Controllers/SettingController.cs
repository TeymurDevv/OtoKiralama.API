using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Setting;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllSettingsAsync(int pageNumber=1, int pageSize=10)
        {
            var settings = await _settingService.GetAllSettingsAsync(pageNumber, pageSize);
            return Ok(settings);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> GetSettingByKeyAsync(string key)
        {
            var setting = await _settingService.GetSettingByKeyAsync(key);
            return Ok(setting);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSettingByIdAsync(int id)
        {
            var setting = await _settingService.GetSettingByIdAsync(id);
            return Ok(setting);
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateSettingAsync(SettingCreateDto settingCreateDto)
        {
            await _settingService.CreateSettingAsync(settingCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSettingAsync(int id, [FromBody] SettingUpdateDto settingUpdateDto)
        {
            await _settingService.UpdateSettingAsync(id,settingUpdateDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
