using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Domain.Repositories;
using System.Security.Claims;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            return Ok(await _authService.LogInAsync(loginDto));
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
           await _authService.RegisterAsync(registerDto);
           return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyPersonelRegister")]
        public async Task<IActionResult> CompanyPersonelRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
            await _authService.CompanyPersonelRegisterAsync(registerCompanyUserDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("CompanyAdminRegister")]
        public async Task<IActionResult> CompanyAdminRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
            await _authService.CompanyAdminRegisterAsync(registerCompanyUserDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("ValidateToken")]
        [Authorize(Roles = "admin")]
        public IActionResult ValidateToken([FromHeader] string Authorization)
        {
            return Ok(_authService.ValidateToken(Authorization));
        }
        [HttpPost("ValidateAgentToken")]
        [Authorize(Roles = "companyAdmin,companyPersonel")]
        public IActionResult ValidateAgentToken([FromHeader] string Authorization)
        {
            return Ok(_authService.ValidateAgentToken(Authorization));
        }
        [HttpPost("ValidateUserToken")]
        [Authorize(Roles = "member")]
        public IActionResult ValidateUserToken([FromHeader] string Authorization)
        {
            return Ok(_authService.ValidateUserToken(Authorization));
        }
    }
}

