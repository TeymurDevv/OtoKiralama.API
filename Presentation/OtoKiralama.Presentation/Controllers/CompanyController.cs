using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCompanies(int pageNumber = 1, int pageSize = 10)
        {
            var companies = await _companyService.GetAllCompaniesAsync(pageNumber, pageSize);
            return Ok(companies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            return Ok(await _companyService.GetCompanyByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            await _companyService.CreateCompanyAsync(companyCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyById(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
