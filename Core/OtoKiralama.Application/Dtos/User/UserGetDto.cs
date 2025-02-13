using OtoKiralama.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Dtos.User
{
    public class UserGetDto
    {
        public string FullName { get; init; }
        public string Id { get; init; }
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public string TcKimlik { get; init; }
        public DateTime? BirthDate { get; set; }
    }
}
