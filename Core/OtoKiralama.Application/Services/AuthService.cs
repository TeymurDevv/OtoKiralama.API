using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
        _unitOfWork = unitOfWork;
    }
    public async Task RegisterAsync(RegisterDto registerDto)
    {
        if (_userManager.Users.Any(u => u.Email == registerDto.Email))
            throw new CustomException(400, "Email", "Email already in use");
        var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
        if (existUser != null) 
            throw new CustomException(400, "Username", "Username already in use");
        AppUser appUser = new AppUser();
        appUser.UserName = registerDto.UserName;
        appUser.Email = registerDto.Email;
        appUser.FullName = registerDto.FullName;
        var result = await _userManager.CreateAsync(appUser, registerDto.Password);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);

            throw new CustomException(400, errorMessages);
        }
        await _userManager.AddToRoleAsync(appUser, "member");
    }

    public async Task CompanyPersonelRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto)
    {
        var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == registerCompanyUserDto.CompanyId);
        if (existCompany is null)
            throw new CustomException(400, "CompanyId", "Company does not exist with this name");
        if (_userManager.Users.Any(u => u.Email == registerCompanyUserDto.Email))
            throw new CustomException(400, "Email", "Email already in use");
        var existUser = await _userManager.FindByNameAsync(registerCompanyUserDto.UserName);
        if (existUser != null)
            throw new CustomException(400, "Username", "Username already in use");
        AppUser appUser = new AppUser();
        appUser.UserName = registerCompanyUserDto.UserName;
        appUser.Email = registerCompanyUserDto.Email;
        appUser.FullName = registerCompanyUserDto.FullName;
        appUser.CompanyId = registerCompanyUserDto.CompanyId;
        var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);

            throw new CustomException(400, errorMessages);
        }        
        await _userManager.AddToRoleAsync(appUser, "companyPersonel");
    }

    public async Task CompanyAdminRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto)
    {
        var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == registerCompanyUserDto.CompanyId);
        if (existCompany is null)
            throw new CustomException(400, "CompanyId", "Company does not exist with this name");
        if (_userManager.Users.Any(u => u.Email == registerCompanyUserDto.Email))
            throw new CustomException(400, "Email", "Email already in use");
        var existUser = await _userManager.FindByNameAsync(registerCompanyUserDto.UserName);
        if (existUser != null)
            throw new CustomException(400, "Username", "Username already in use");
        AppUser appUser = new AppUser();
        appUser.UserName = registerCompanyUserDto.UserName;
        appUser.Email = registerCompanyUserDto.Email;
        appUser.FullName = registerCompanyUserDto.FullName;
        appUser.CompanyId = registerCompanyUserDto.CompanyId;
        var result = await _userManager.CreateAsync(appUser, registerCompanyUserDto.Password);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);

            throw new CustomException(400, errorMessages);
        }        
        await _userManager.AddToRoleAsync(appUser, "companyAdmin");
    }

    public Task LogInAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task ValidateToken(string Authorization)
    {
        throw new NotImplementedException();
    }

    public Task ValidateAgentToken(string Authorization)
    {
        throw new NotImplementedException();
    }

    public Task ValidateUserToken(string Authorization)
    {
        throw new NotImplementedException();
    }
}