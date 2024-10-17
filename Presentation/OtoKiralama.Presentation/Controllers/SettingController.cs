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
        public async Task<IActionResult> GetSettingByIdAsync(string key)
        {
            var setting = await _settingService.GetSettingByIdAsync(key);
            return Ok(setting);
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateSettingAsync(SettingCreateDto settingCreateDto)
        {
            await _settingService.CreateSettingAsync(settingCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
