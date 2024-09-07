using Microsoft.AspNetCore.Identity;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Persistance.Entities
{
    public class AppUser : IdentityUser, IAppUser
    {
        public string FullName { get; set; }
    }
}
