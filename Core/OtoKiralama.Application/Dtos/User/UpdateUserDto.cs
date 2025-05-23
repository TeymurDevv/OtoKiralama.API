﻿using OtoKiralama.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Dtos.User
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public DateTime? BirthDate {  get; set; }
        public string TcKimlik { get; set; }
        public string? PhoneNumber { get; set;}
        public string Email { get; set; }
    }
}
