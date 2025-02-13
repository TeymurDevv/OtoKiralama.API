using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Country;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateCountryAsync(CountryCreateDto countryCreateDto)
        {
            await _countryService.CreateCountryAsync(countryCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCountries(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _countryService.GetAllCountriesAsync(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            return Ok(await _countryService.GetCountryByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryById(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, CountryUpdateDto countryUpdateDto)
        {
            await _countryService.UpdateCountryAsync(id, countryUpdateDto);
            return NoContent();
        }
    }
}
