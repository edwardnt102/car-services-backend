﻿namespace Services.Shared
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }
        public string DomainFile { get; set; }
    }
}
