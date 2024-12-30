using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Persistance.Data.Implementations;
using OtoKiralama.Persistance.Entities; 
using System.Security.Claims;

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
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(_userManager.Users.Any(u => u.Email == registerDto.Email))
                throw new CustomException(400,"Email", "Email already in use");
            if(_userManager.Users.Any(u => u.UserName == registerDto.UserName))
                throw new CustomException(400,"Username", "Username already in use");
            var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existUser != null) return BadRequest();
            AppUser appUser = new AppUser();
            appUser.UserName = registerDto.UserName;
            appUser.Email = registerDto.Email;
            appUser.FullName = registerDto.FullName;
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);

                throw new CustomException(400, errorMessages);
            }            await _userManager.AddToRoleAsync(appUser, "member");
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyPersonelRegister")]
        public async Task<IActionResult> CompanyPersonelRegister(RegisterCompanyUserDto registerCompanyUserDto)
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
            appUser.CompanyId = registerCompanyUserDto.CompanyId;
            var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "companyPersonel");
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyAdminRegister")]
        public async Task<IActionResult> CompanyAdminRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
            var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == registerCompanyUserDto.CompanyId);
            if (existCompany is null)
                throw new CustomException(400, "CompanyId", "Company does not exist with this Id");
            var existUser = await _userManager.FindByNameAsync(registerCompanyUserDto.UserName);
            if (existUser != null)
                throw new CustomException(400, "UserName", "User already exist with this name");
            AppUser appUser = new AppUser();
            appUser.UserName = registerCompanyUserDto.UserName;
            appUser.Email = registerCompanyUserDto.Email;
            appUser.FullName = registerCompanyUserDto.FullName;
            appUser.CompanyId = registerCompanyUserDto.CompanyId;
            var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(appUser, "companyAdmin");
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
            await _roleManager.CreateAsync(new IdentityRole("companyAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("companyPersonel"));
            return Ok();
        }
        
        [HttpPost("ValidateToken")]
        [Authorize(Roles ="admin")]
        public IActionResult ValidateToken([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Invalid token format." });
            }

            var token = Authorization.Substring("Bearer ".Length).Trim();
            var principal = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid or expired token." });
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var user = _userManager.Users.FirstOrDefault(u=>u.Id==userId);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            return Ok(new
            {
                id = user.Id,
                username = user.UserName,
                first_name = user.FullName,
                last_name = user.FullName,
                email = user.Email,
                roles = "admin",
            });
        }
        
        [HttpPost("ValidateAgentToken")]
        [Authorize(Roles = "companyAdmin,companyPersonel")]
        public IActionResult ValidateAgentToken([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Invalid token format." });
            }

            var token = Authorization.Substring("Bearer ".Length).Trim();
            var principal = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid or expired token." });
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            return Ok(new
            {
                id = user.Id,
                username = user.UserName,
                first_name = user.FullName,
                last_name = user.FullName,
                email = user.Email,
                companyId = user.CompanyId,
                roles = "companyAdmin",
            });
        }
        
        [HttpPost("ValidateUserToken")]
        [Authorize(Roles = "member")]
        public IActionResult ValidateUserToken([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Invalid token format." });
            }

            var token = Authorization.Substring("Bearer ".Length).Trim();
            var principal = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid or expired token." });
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            return Ok(new
            {
                id = user.Id,
                username = user.UserName,
                first_name = user.FullName,
                last_name = user.FullName,
                email = user.Email,
                roles = "member",
            });
        }
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
            var existedUser=await _userManager.FindByIdAsync(userId);
            if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
            var mappedUser=_mapper.Map<UserGetDto>(existedUser);
            return Ok(mappedUser);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {

            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
            var existedUser = await _userManager.FindByIdAsync(userId);
            if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
            if (updateUserDto.UserIdentityInformation.Value != UserIdentityInformation.TCKimlik && updateUserDto.UserIdentityInformation != UserIdentityInformation.Passport)
                throw new CustomException(400, "UsUserIdentityInformation", "UsUserIdentityInformation is wrong");
            _mapper.Map(updateUserDto, existedUser);
            var result = await _userManager.UpdateAsync(existedUser);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new CustomException(400, "Update Failed", errors);
            }
            var mappedExistedUser=_mapper.Map<UserGetDto>(existedUser);
            return Ok(mappedExistedUser);
        }
    }
}

