using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly IEmailService _emailService;
    public AuthService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        
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

    public async Task<AuthResponseDto> LogInAsync(LoginDto loginDto)
    {
        var existUser = await _userManager.FindByNameAsync(loginDto.UserName);
        var result = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
        if (!result)
            throw new CustomException(400, "Password", "Password is incorrect");
        IList<string> roles = await _userManager.GetRolesAsync(existUser);
        var Audience = _jwtSettings.Audience;
        var SecretKey = _jwtSettings.secretKey;
        var Issuer = _jwtSettings.Issuer;
        AuthResponseDto authResponseDto = new()
        {
            Token = _tokenService.GetToken(SecretKey, Audience, Issuer, existUser, roles)
        };
        return authResponseDto;
    }

    public async Task<TokenValidationReturnDto> ValidateToken([FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            throw new CustomException(401, "Authorization", "Authorization header is not valid");

        var token = Authorization.Substring("Bearer ".Length).Trim();
        var principal = _tokenService.ValidateToken(token);

        if (principal == null)
            throw new CustomException(401, "Authorization", "Token is not valid");

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new CustomException(401, "Authorization", "User id is not found");

        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new CustomException(401, "Authorization", "User is not found");
        TokenValidationReturnDto tokenValidationReturnDto = new()
        {
            email = user.Email,
            first_name = user.FullName,
            id = user.Id,
            last_name = user.FullName,
            roles = "admin",
            username = user.UserName
        };
        return tokenValidationReturnDto;
    }

    public async Task<TokenValidationReturnDto> ValidateAgentToken([FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            throw new CustomException(401, "Authorization", "Authorization header is not valid");

        var token = Authorization.Substring("Bearer ".Length).Trim();
        var principal = _tokenService.ValidateToken(token);

        if (principal == null)
            throw new CustomException(401, "Authorization", "Token is not valid");

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new CustomException(401, "Authorization", "User id is not found");

        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            throw new CustomException(401, "Authorization", "User is not found");
        TokenValidationReturnDto tokenValidationReturnDto = new()
        {
            email = user.Email,
            first_name = user.FullName,
            id = user.Id,
            last_name = user.FullName,
            roles = "companyAdmin",
            username = user.UserName
        };
        return tokenValidationReturnDto;
    }

    public async Task<TokenValidationReturnDto> ValidateUserToken([FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
            throw new CustomException(401, "Authorization", "Authorization header is not valid");

        var token = Authorization.Substring("Bearer ".Length).Trim();
        var principal = _tokenService.ValidateToken(token);

        if (principal == null)
            throw new CustomException(401, "Authorization", "Token is not valid");

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new CustomException(401, "Authorization", "User id is not found");

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new CustomException(401, "Authorization", "User is not found");
        TokenValidationReturnDto tokenValidationReturnDto = new()
        {
            email = user.Email,
            first_name = user.FullName,
            id = user.Id,
            last_name = user.FullName,
            roles = "member",
            username = user.UserName
        };
        return tokenValidationReturnDto;
    }

    public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var existUser = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (existUser is null)
            throw new CustomException(400, "User", "User not found");
        var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
        existUser.PasswordResetToken = token;
        existUser.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(5); // Token 5 deqiqe muddetinde aktivdir.
        await _userManager.UpdateAsync(existUser);
        var resetLink = $"http://localhost:8080/reset-password?email={forgotPasswordDto.Email}&token={Uri.EscapeDataString(token)}";
        await _emailService.SendEmailAsync(forgotPasswordDto.Email, "Reset Password", $"Please reset your password by clicking this link: <a href='{resetLink}'>Reset Password</a>");
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null)
            throw new CustomException(400, "User", "User not found");
        
        if (user.PasswordResetTokenExpiry == null || DateTime.UtcNow > user.PasswordResetTokenExpiry)
            throw new CustomException(400, "User", "Token is expired");
        
        if (user.PasswordResetToken != resetPasswordDto.Token)
            throw new CustomException(400, "User", "Token is invalid");
        
        var resetResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
        
        if (!resetResult.Succeeded)
        {
            var errors = resetResult.Errors.Select(e => e.Description);
            throw new CustomException(400, "ResetPassword", string.Join(", ", errors));
        }
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        await _userManager.UpdateAsync(user);
    }
}