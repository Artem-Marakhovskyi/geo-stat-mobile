using System;
using System.Text.RegularExpressions;

namespace GeoStat.Common.Services
{
    public class ValidationService : IValidationService
    {
        private static readonly Regex _emailRegex = new Regex(@"^([\w-\.]+)@(\w+\.){1,2}[a-zA-Z]{2,3}$");
        private static readonly Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var match = _emailRegex.Match(email);

            return match.Success;
        }

        public bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var match = _passwordRegex.Match(password);

            return match.Success;
        }
    }
}
