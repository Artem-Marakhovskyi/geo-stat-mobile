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
        public IMvxCommand ReadCommand
        {
            get
            {
                async void execute() => await Read();
                return new MvxCommand(execute);
            }
        }
        public IMvxCommand InsertCommand => new MvxCommand(async () => await InsertLocation());

        private readonly ICloudService _cloudService;

        public HomeViewModel(ICloudService cloudService)
        {
            _cloudService = cloudService;
        }

        private async Task<Location> InsertLocation()
        {
            var locations = await _cloudService.GetTableAsync<Location>();

            return await locations.CreateItemAsync(new Location
            {
                Latitude = 1.2,
                Longitude = 1.3,
                UserId = "",
                DateTime = DateTimeOffset.Now
            });
        }

        private async Task Read()
        {
            await _cloudService.SyncOfflineCacheAsync();
            var locations = await _cloudService.GetTableAsync<Location>();
            var list = await locations.ReadAllItemsAsync();

            foreach (var item in list)
                Console.WriteLine(item);
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
