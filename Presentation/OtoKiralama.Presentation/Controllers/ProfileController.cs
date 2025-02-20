using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.User;
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
            return Ok(await _profileService.GetUserInformationAsync());
        }

        [HttpDelete("DeleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            await _profileService.DeleteUser();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpGet("GetUserReservations")]
        [Authorize]
        public async Task<IActionResult> GetUserReservations(int pageNumber = 1, int pageSize = 10)
        {
            var reservations = await _profileService.GetUserReservations(pageNumber, pageSize);
            return Ok(reservations);
        }

        [HttpPut("ChangeSubscribtionStatus")]
        [Authorize]
        public async Task<IActionResult> ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
        {
            await _profileService.ChangeSubscribtionStatus(changeSubscribtionStatusDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut("UpdateUserInformation")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInformation(UpdateUserDto updateUserDto)
        {
            await _profileService.UpdateUserInformation(updateUserDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
