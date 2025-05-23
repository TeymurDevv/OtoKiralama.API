﻿using OtoKiralama.Application.Dtos.Company;

namespace OtoKiralama.Application.Dtos.User
{
    public class UserListItemDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public CompanyReturnDto Company { get; set; }
    }
}
