using System;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using GeoStat.Common.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);

        public HomeViewModel()
        {
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
