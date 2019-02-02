using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace GeoStatMobile.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissions _permissions;

        public PermissionService(
           IPermissions permissions)
        {
            _permissions = permissions;
        }

        public Task<PermissionStatus> CheckPermissionStatusAsync(Permission featurePermission) 
            => _permissions.CheckPermissionStatusAsync(featurePermission);

        public async Task<PermissionStatus> RequestPermissionAsync(Permission featurePermission)
        {
            var status = await _permissions.CheckPermissionStatusAsync(featurePermission);

            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                var results = await _permissions.RequestPermissionsAsync(featurePermission);

                if (results.ContainsKey(featurePermission))
                {
                    status = results[featurePermission];
                }
            }

            return status;
        }
    }
}
