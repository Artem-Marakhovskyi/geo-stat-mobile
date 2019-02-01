using System;
using System.Text.RegularExpressions;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Services
{
    public class ValidationService : IValidationService
    {
        private static Regex _emailRegex = new Regex(@"^([\w-\.]+)@(\w+\.){1,2}[a-zA-Z]{2,3}$");
        private static Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");

        public ValidationService()
        {
        }

        public bool IsEmailValid(string email)
        {
            if (email == null) return false;

            Match match = _emailRegex.Match(email);

            return match.Success;
        }

        public bool IsPasswordValid(string password)
        {
            if (password == null) return false;

            Match match = _passwordRegex.Match(password);

            return match.Success;
        }
    }
}
