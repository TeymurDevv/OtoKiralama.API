using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
using System.Security.Claims;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        [HttpGet("GetUserInformation")]
        [Authorize]
        public async Task<IActionResult> GetUserInformation()
        {
        }

        [HttpDelete("DeleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
        }
        [HttpGet("GetUserReservations")]
        [Authorize]
        public async Task<IActionResult> GetUserReservations(int pageNumber, int pageSize)
        {
        }

        [HttpPut("ChangeSubscribtionStatus")]
        [Authorize]
        public async Task<IActionResult> ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
        {
  
        }
    }
}
