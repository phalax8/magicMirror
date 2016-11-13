using magicMirror.Data;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace magicMirror.Controls
{
    public sealed partial class WeatherControl : UserControl
    {

        WeatherCtrl weather = new WeatherCtrl();

        public WeatherControl()
        {
            this.InitializeComponent();

            this.DataContext = weather;

            TimeSpan period = TimeSpan.FromHours(1);
            Task refreshTask = PeriodicallyRefreshDataAsync(period);
        }

        private async Task PeriodicallyRefreshDataAsync(TimeSpan period)
        {
            while (true)
            {
                await weather.GetWeather();
                await Task.Delay(period);
            }
        }

        
    }
}