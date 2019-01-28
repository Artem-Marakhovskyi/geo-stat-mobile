using System;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;

namespace GeoStat.Common.Services
{
    public class UserService
    {
        private readonly IGeoStatRepository<GeoStatUser> _userRepository;

        public UserService(IGeoStatRepository<GeoStatUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Register()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Login()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
