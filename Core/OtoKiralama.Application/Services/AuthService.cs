using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.User;
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
    public Task RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
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