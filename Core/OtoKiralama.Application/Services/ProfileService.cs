using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;
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
    public Task<UserGetDto> GetUserInformationAsync()
    {
        throw new NotImplementedException();
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