﻿namespace KSA_API.Models
{
    public class ApiLogin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int TokenLifetime { get; set; }
    }
}
