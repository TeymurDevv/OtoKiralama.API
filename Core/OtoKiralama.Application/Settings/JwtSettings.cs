﻿namespace OtoKiralama.Application.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string secretKey { get; set; }
    }
}
