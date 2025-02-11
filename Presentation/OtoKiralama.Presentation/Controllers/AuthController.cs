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
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
        }
        [HttpPost("CompanyPersonelRegister")]
        public async Task<IActionResult> CompanyPersonelRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
        }
        [HttpPost("CompanyAdminRegister")]
        public async Task<IActionResult> CompanyAdminRegister(RegisterCompanyUserDto registerCompanyUserDto)
        {
        }
        [HttpPost("ValidateToken")]
        [Authorize(Roles = "admin")]
        public IActionResult ValidateToken([FromHeader] string Authorization)
        {
        }
        [HttpPost("ValidateAgentToken")]
        [Authorize(Roles = "companyAdmin,companyPersonel")]
        public IActionResult ValidateAgentToken([FromHeader] string Authorization)
        {
        }
        [HttpPost("ValidateUserToken")]
        [Authorize(Roles = "member")]
        public IActionResult ValidateUserToken([FromHeader] string Authorization)
        {
        }
    }
}

