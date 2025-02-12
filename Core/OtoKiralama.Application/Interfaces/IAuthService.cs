using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task CompanyPersonelRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto);
    Task CompanyAdminRegisterAsync(RegisterCompanyUserDto registerCompanyUserDto);
    Task<AuthResponseDto> LogInAsync(LoginDto loginDto);
    Task<TokenValidationReturnDto> ValidateToken([FromHeader] string Authorization);
    Task<TokenValidationReturnDto> ValidateAgentToken([FromHeader] string Authorization);
    Task<TokenValidationReturnDto> ValidateUserToken([FromHeader] string Authorization);
    Task ForgotPassword(ForgotPasswordDto forgotPasswordDto);
    Task ResetPassword(ResetPasswordDto resetPasswordDto);
}