using magicMirror.Data;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace magicMirror.Controls
{
    public sealed partial class DateTimeControl : UserControl
    {

        CultureInfo culture = new System.Globalization.CultureInfo("es-ES");
        DateTimeCtrl dateTime = new DateTimeCtrl();

        public DateTimeControl()
        {
            this.InitializeComponent();

            this.DataContext = dateTime;

            dateTime.dayValue = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).ToString();
            dateTime.dateValue = DateTime.Today.ToString("dd MMMM, yyyy", culture);

            TimeSpan period = TimeSpan.FromSeconds(1);
            Task refreshTask = PeriodicallyRefreshDataAsync(period);

        }

        private async Task PeriodicallyRefreshDataAsync(TimeSpan period)
        {
            while (true)
            {
                dateTime.timeValue = DateTime.Now.ToString("HH:mm:ss");
                await Task.Delay(period);
            }
        }

    }
}
