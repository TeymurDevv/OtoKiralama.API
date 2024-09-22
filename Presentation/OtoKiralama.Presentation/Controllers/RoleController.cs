using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            return Ok(await _roleService.GetRoleByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateRole(RoleCreateDto roleCreateDto)
        {
            await _roleService.CreateRoleAsync(roleCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
