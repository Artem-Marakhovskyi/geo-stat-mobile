using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace GeoStatMobile.Services
{
    public interface IPermissionService
    {
        Task<PermissionStatus> CheckPermissionStatusAsync(Permission featurePermission);
        Task<PermissionStatus> RequestPermissionAsync(Permission featurePermission);
    }
}