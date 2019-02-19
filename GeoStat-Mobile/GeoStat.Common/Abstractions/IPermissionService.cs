using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace GeoStat.Common.Abstractions
{
    public interface IPermissionService
    {
        Task<PermissionStatus> CheckPermissionStatusAsync(Permission featurePermission);
        Task<PermissionStatus> RequestPermissionAsync(Permission featurePermission);
    }
}