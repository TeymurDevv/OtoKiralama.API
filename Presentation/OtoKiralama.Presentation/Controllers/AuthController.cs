using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Persistance.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null) return BadRequest();
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) return BadRequest();
            
            //jwt
            var handler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256);

            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaim(new Claim("Address", "Turkey"));

            var roles = await _userManager.GetRolesAsync(user);
            ci.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddHours(1),
                Subject = ci,
                Audience = _configuration.GetSection("Jwt:Audience").Value,
                Issuer = _configuration.GetSection("Jwt:Issuer").Value,
                NotBefore = DateTime.Now
            };

            var token = handler.CreateToken(tokenDescriptor);
            return Ok(new { token = handler.WriteToken(token) });
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
