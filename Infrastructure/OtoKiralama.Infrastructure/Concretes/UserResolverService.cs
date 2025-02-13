using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using System.Security.Claims;

namespace OtoKiralama.Infrastructure.Concretes
{
    public class UserResolverService : IUserResolverService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string?> GetCurrentUserIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new CustomException(401, "UserId", "Please login system");
            }
            return userId;
        }

    }
}
