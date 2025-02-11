using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Application.Services;

public class AuthService : IAuthService
{
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