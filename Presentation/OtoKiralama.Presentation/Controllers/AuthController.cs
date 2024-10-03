using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Persistance.Data.Implementations;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existUser != null) return BadRequest();
            AppUser appUser = new AppUser();
            appUser.UserName = registerDto.UserName;
            appUser.Email = registerDto.Email;
            appUser.FullName = registerDto.FullName;
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "member");
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyPersonelRegister")]
        public async Task<IActionResult> CompanyPersonelRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
            var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c=>c.Id==registerCompanyUserDto.CompanyId);
            if (existCompany is null)
                throw new CustomException(400, "CompanyId", "Company does not exist with this name");
            var existUser = await _userManager.FindByNameAsync(registerCompanyUserDto.UserName);
            if (existUser != null) return BadRequest();
            AppUser appUser = new AppUser();
            appUser.UserName = registerCompanyUserDto.UserName;
            appUser.Email = registerCompanyUserDto.Email;
            appUser.FullName = registerCompanyUserDto.FullName;
            var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "agentPersonel");
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyAdminRegister")]
        public async Task<IActionResult> CompanyAdminRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
            var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == registerCompanyUserDto.CompanyId);
            if (existCompany is null)
                throw new CustomException(400, "CompanyId", "Company does not exist with this name");
            var existUser = await _userManager.FindByNameAsync(registerCompanyUserDto.UserName);
            if (existUser != null) return BadRequest();
            AppUser appUser = new AppUser();
            appUser.UserName = registerCompanyUserDto.UserName;
            appUser.Email = registerCompanyUserDto.Email;
            appUser.FullName = registerCompanyUserDto.FullName;
            var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "agentAdmin");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            var existUser = await _userManager.FindByNameAsync(loginDto.UserName);
            if (existUser == null) return BadRequest();
            var result = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
            if (!result) return BadRequest();
            IList<string> roles = await _userManager.GetRolesAsync(existUser);
            var Audience = _jwtSettings.Audience;
            var SecretKey = _jwtSettings.secretKey;
            var Issuer = _jwtSettings.Issuer;
            return Ok(new { token = _tokenService.GetToken(SecretKey, Audience, Issuer, existUser, roles) });

        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {

            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("member"));
            await _roleManager.CreateAsync(new IdentityRole("companyPersonel"));
            await _roleManager.CreateAsync(new IdentityRole("companyPersonel"));
            return Ok();
        }
    }
}
