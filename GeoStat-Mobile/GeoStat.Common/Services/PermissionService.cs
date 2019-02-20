using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using MvvmCross.Logging;
using Plugin.Permissions.Abstractions;

namespace GeoStat.Common.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMvxLog _log;
        private readonly IPermissions _permissions;

        public PermissionService(
           IPermissions permissions,
           IMvxLog log)
        {
            _log = log;
            _permissions = permissions;
        }

        public Task<PermissionStatus> CheckPermissionStatusAsync(Permission featurePermission)
            => _permissions.CheckPermissionStatusAsync(featurePermission);

        public async Task<PermissionStatus> RequestPermissionAsync(Permission featurePermission)
        {
            var status = await _permissions.CheckPermissionStatusAsync(featurePermission);
            try
            {
                if (status != PermissionStatus.Granted)
                {
                    var results = await _permissions.RequestPermissionsAsync(featurePermission);

                    if (results.ContainsKey(featurePermission))
                    {
                        status = results[featurePermission];
                    }
                }
            }
            catch (TaskCanceledException)
            {
                _log.Warn("No permissions are added");
            }

            return status;
        }
    }
}
