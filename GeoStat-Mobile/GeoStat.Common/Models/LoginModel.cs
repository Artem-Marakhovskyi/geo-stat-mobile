﻿using System;
namespace GeoStat.Common.Models
{
    public class LoginModel
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
