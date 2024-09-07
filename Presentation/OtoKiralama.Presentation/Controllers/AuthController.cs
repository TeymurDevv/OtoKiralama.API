using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existUser is not null)
                return BadRequest();
            AppUser user = new()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(user, "member");

            return StatusCode(201);
        }
        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("admin"))
        //        await _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
        //    if (!await _roleManager.RoleExistsAsync("member"))
        //        await _roleManager.CreateAsync(new IdentityRole() { Name = "member"});
        //    return StatusCode(201);
        //}
    }
}
