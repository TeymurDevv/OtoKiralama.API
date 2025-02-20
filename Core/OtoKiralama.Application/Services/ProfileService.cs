using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserResolverService _userResolverService;

    public ProfileService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userResolverService = userResolverService;
    }
    public async Task<UserGetDto> GetUserInformationAsync()
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
        var mappedUser = _mapper.Map<UserGetDto>(existedUser);
        return mappedUser;
    }

    public async Task DeleteUser()
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existUser = await _userManager.FindByIdAsync(userId);
        if (existUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur");
        await _userManager.DeleteAsync(existUser);
    }

    public async Task<PagedResponse<ReservationListItemDto>> GetUserReservations(int pageNumber, int pageSize)
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
        var existedReservations = await _unitOfWork.ReservationRepository.GetAll(
            r => r.AppUserId == userId,
            includes: query => query
                    .Include(c => c.Car)
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
                    .Include(c => c.AppUser)
                    .Include(c => c.DropOfLocation)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
            );

        var totalReservations = await _unitOfWork.ReservationRepository.CountAsync(r => r.AppUserId == userId); //bug report burda biz butun reservationlari sayiriq amma userin reservationlari sayilmalidir...
        return new PagedResponse<ReservationListItemDto>
        {
            Data = _mapper.Map<List<ReservationListItemDto>>(existedReservations),
            TotalCount = totalReservations,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }


    public async Task ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
        existedUser.IsEmailSubscribed = changeSubscribtionStatusDto.IsEmailSubscribed;
        existedUser.IsSmsSubscribed = changeSubscribtionStatusDto.IsSmsSubscribed;
        await _userManager.UpdateAsync(existedUser);
    }

    public async Task UpdateUserInformation(UpdateUserDto updateUserDto)
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
        _mapper.Map(updateUserDto, existedUser);
        var result = await _userManager.UpdateAsync(existedUser);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CustomException(400, "Update Failed", errors);
        }
    }
}