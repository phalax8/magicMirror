using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace magicMirror.Converters
{
    class formatTemperature : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            float temp = float.Parse((string) value);
            return temp.ToString("0.0") + "º";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
