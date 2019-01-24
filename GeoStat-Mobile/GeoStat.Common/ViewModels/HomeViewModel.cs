using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using GeoStat.Common.Services;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        public IMvxCommand ReadCommand => new MvxCommand(async () => await Read());
        public IMvxCommand InsertCommand => new MvxCommand(async () => await InsertLocation());

        private readonly ICloudService _cloudService;

        public HomeViewModel(ICloudService cloudService)
        {
            _cloudService = cloudService;
        }

        private async Task InsertLocation()
        {
            var repository = new GeoStatRepository<Location>(_cloudService);
            var locationService = new LocationService(repository);

            await locationService.AddLocationAsync(new LocationModel(112, 21, DateTimeOffset.UtcNow));
        }

        private async Task Read()
        {
            await _cloudService.SyncOfflineCacheAsync();

            var repository = new GeoStatRepository<Location>(_cloudService);
            var locationService = new LocationService(repository);

            var list = await locationService.GetLocationsByIdAsync(UserContext.UserId);
        }

        private void ResetText()
        {
            Text = "Hello MvvmCross";
        }

        private string _text = "Hello MvvmCross";

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
