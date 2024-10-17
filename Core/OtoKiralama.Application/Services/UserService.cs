using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new CustomException(400, "userId", "User not found with this Id");

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new CustomException(400, "userId", "User could not be deleted");
        }

        public async Task<PagedResponse<UserListItemDto>> GetAllUsers(int pageNumber, int pageSize)
        {
            List<AppUser> users = await _userManager
                .Users
                .Include(u => u.Company)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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
