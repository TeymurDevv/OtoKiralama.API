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

    public Task CompanyPersonelRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto)
    {
        throw new NotImplementedException();
    }

    public Task CompanyAdminRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto)
    {
        throw new NotImplementedException();
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