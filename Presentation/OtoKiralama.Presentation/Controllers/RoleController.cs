using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
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
    }
}
