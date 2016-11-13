using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magicMirror.Data
{
    class DateTimeCtrl : ObservableObject
    {
        String _dayValue;
        public String dayValue
        {
            get { return _dayValue; }
            set { 
                _dayValue = value;
                OnPropertyChanged("dayValue");
            }
        }

        String _timeValue;
        public String timeValue
        {
            get { return _timeValue; }
            set
            {
                _timeValue = value;
                OnPropertyChanged("timeValue");
            }
        }

        String _dateValue;
        public String dateValue
        {
            get { return _dateValue; }
            set
            {
                _dateValue = value;
                OnPropertyChanged("dateValue");
            }
        }

    }
}
