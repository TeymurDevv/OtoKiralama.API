using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async void DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new CustomException(400, "userId", "User not found with this Id");
            await _userManager.DeleteAsync(user);
        }

        public async Task<PagedResponse<UserListItemDto>> GetAllUsers(int pageNumber, int pageSize)
        {
            List < AppUser > users  = await _userManager
                .Users
                .Include(u => u.Company)
                .ToListAsync();
            var totalUsers = await _userManager.Users.CountAsync();
            

            return new PagedResponse<UserListItemDto>
            {
                Data = _mapper.Map<List<UserListItemDto>>(users),
                TotalCount = totalUsers,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<UserReturnDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new CustomException(400, "userId", "User not found with this Id");
            return _mapper.Map<UserReturnDto>(user);
        }
    }
}
