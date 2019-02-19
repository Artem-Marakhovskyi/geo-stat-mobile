using System;
namespace GeoStat.Common.Models
{
    public class RegisterModel
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string RepeatedPassword { get; private set; }

        public RegisterModel(string email, string password, string repeatedPassword)
        {
            Email = email;
            Password = password;
            RepeatedPassword = repeatedPassword;
        }
    }
}
