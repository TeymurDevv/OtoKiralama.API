using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task CompanyPersonelRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto);
    Task CompanyAdminRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto);
    Task<AuthResponseDto> LogInAsync(LoginDto loginDto);
    Task ValidateToken([FromHeader] string Authorization);
    Task ValidateAgentToken([FromHeader] string Authorization);
    Task ValidateUserToken([FromHeader] string Authorization);
}