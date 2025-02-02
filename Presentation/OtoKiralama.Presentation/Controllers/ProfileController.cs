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
using OtoKiralama.Persistance.Data.Implementations;
using OtoKiralama.Persistance.Entities;
using System.Security.Claims;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProfileController(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet("GetUserInformation")]
        [Authorize]
        public async Task<IActionResult> GetUserInformation()
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
            var existedUser = await _userManager.FindByIdAsync(userId);
            if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
            var mappedUser = _mapper.Map<UserGetDto>(existedUser);
            return Ok(mappedUser);
        }
        [HttpGet("GetUserReservations")]
        [Authorize]
        public async Task<IActionResult> GetUserReservations(int pageNumber, int pageSize)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
            var existedUser = await _userManager.FindByIdAsync(userId);
            if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
            var existedReservations = await _unitOfWork.ReservationRepository.GetAll(s => s.AppUserId == userId, includes: new Func<IQueryable<Reservation>, IQueryable<Reservation>>[]
    {
        query => query.Include(c => c.Car)
                        .ThenInclude(c => c.Model)
                        .ThenInclude(c => c.Brand)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Model)
                            .ThenInclude(m => m.CarPhoto)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Body)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Class)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Fuel)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Gear)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Location)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Company)
                    .Include(c => c.AppUser).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
    });
            var mappedReservation = _mapper.Map<List<ReservationListItemDto>>(existedReservations);
            var totalReservations = await _unitOfWork.ReservationRepository.CountAsync();
            return Ok(new PagedResponse<ReservationListItemDto>
            {
                Data = mappedReservation,
                TotalCount = totalReservations,
                PageSize = pageSize,
                CurrentPage = pageNumber
            });
        }

        [HttpPut("ChangeSubscribtionStatus")]
        [Authorize]
        public async Task<IActionResult> ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
            var existedUser = await _userManager.FindByIdAsync(userId);
            if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
            existedUser.IsEmailSubscribed = changeSubscribtionStatusDto.IsEmailSubscribed;
            existedUser.IsSmsSubscribed = changeSubscribtionStatusDto.IsSmsSubscribed;
            await _userManager.UpdateAsync(existedUser);
            return Ok(StatusCodes.Status200OK);
        }
    }
}
