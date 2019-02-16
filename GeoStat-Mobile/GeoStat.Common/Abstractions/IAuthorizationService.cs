using System;
using System.Threading.Tasks;
using GeoStat.Common.Models;

namespace GeoStat.Common.Abstractions
{
    public interface IAuthorizationService
    {
        Task SendRequestForLogin(LoginModel loginModel);
        Task SendRequestForRegister(RegisterModel registerModel);
    }
}
