using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;

        public CompanyController(ICompanyService companyService, UserManager<AppUser> userManager)
        {
            _companyService = companyService;
            _userManager = userManager;
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

        [HttpGet("GetCompanyByName/{name}")]
        public async Task<IActionResult> GetCompanyByName(string name)
        {
            return Ok(await _companyService.GetCompanyByNameAsync(name));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            await _companyService.CreateCompanyAsync(companyCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompanyById(int id)
        //{
        //    List<AppUser> companyUsers = await _userManager
        //        .Users
        //        .Where(u => u.CompanyId == id)
        //        .ToListAsync();
        //    foreach (var user in companyUsers)
        //    {
        //        await _userManager.DeleteAsync(user);
        //    }
        //    await _companyService.DeleteCompanyAsync(id);
        //    return StatusCode(StatusCodes.Status204NoContent);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, string userId, CompanyUpdateDto companyUpdateDto)
        {
            await _companyService.UpdateCompanyAsync(id, userId, companyUpdateDto);
            return NoContent();
        }
    }
}
