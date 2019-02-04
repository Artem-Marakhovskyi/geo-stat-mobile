using System;
using System.Globalization;
using MvvmCross.Plugin.Visibility;
using MvvmCross.UI;

namespace GeoStat.Droid
{
    public class InvertedVisibilityValueConverter : MvxBaseVisibilityValueConverter<bool>
    {
        protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? MvxVisibility.Hidden : MvxVisibility.Visible;
        }
    }
}
