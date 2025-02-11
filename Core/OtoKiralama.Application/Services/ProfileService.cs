using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    private readonly IHttpContextAccessor _contextAccessor;

    public ProfileService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _contextAccessor = httpContextAccessor;
    }
    public async Task<UserGetDto> GetUserInformationAsync()
    {
        var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) throw new CustomException(401, "UserId", "Kullanici id bos gelemez");
        var existedUser = await _userManager.FindByIdAsync(userId);
        if (existedUser is null) throw new CustomException(404, "User", " Boyle kullanici yoktur ");
        var mappedUser = _mapper.Map<UserGetDto>(existedUser);
        return mappedUser;
    }

    public Task DeleteUser()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<ReservationListItemDto>> GetUserReservations(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
    {
        throw new NotImplementedException();
    }
}