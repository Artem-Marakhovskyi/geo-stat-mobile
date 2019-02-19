using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Models;

namespace GeoStat.Common.Abstractions
{
    public interface IUserService
    {
        Task Register(RegisterModel model);
        Task Login(LoginModel model);

        Task<ICollection<GroupModel>> GetGroupsOfUser();
    }
}
