using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace magicMirror.Data
{
    class WeatherCtrl : ObservableObject
    {

        const string APIOPENWEATHER = "c34338a06a3af0b4f691ca23668160eb";

        String _language = "es";
        public String Language
        {
            get { return _language; }
            set
            {
                _language = value;
                //OnPropertyChanged("Language");
            }
        }

        String _city = "errentería";
        public String City
        {
            get { return _city; }
            set
            {
                _city = value;
                //OnPropertyChanged("City");
            }
        }

        Weather _Weather_now;
        public Weather Weather_now
        {
            get { return _Weather_now; }
            set
            {
                _Weather_now = value;
                OnPropertyChanged("Weather_now");
            }
        }

        public List<Weather> Weather_next;

        public async Task GetWeather()
        {
            Uri uri = new Uri("http://api.openweathermap.org/data/2.5/forecast/city?q=" + City + "&APPID=" + APIOPENWEATHER + "&units=metric&lang=" + Language + "&mode=json");
            HttpClient httpClient = new HttpClient();

            try
            {
                String json = await httpClient.GetStringAsync(uri);
                dynamic result = JObject.Parse(json);
                DateTimeOffset dateTimeOffset;
                DateTime dateTime;
                DayOfWeek day_of_week = DayOfWeek.Monday;
                int index = 0;

                JArray list = (JArray)result["list"];   

                foreach (var item in list)
                {
                    dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)item["dt"]);
                    dateTime = dateTimeOffset.DateTime;

                    switch (index)
                    {
                        case 0:
                            day_of_week = dateTime.DayOfWeek;
                            Weather_now = new Weather(
                                (string) item["main"]["temp"],
                                (string) item["main"]["temp_min"],
                                (string) item["main"]["temp_max"],
                                (string) item["weather"][0]["description"],
                                day_of_week.ToString()
                            );
                            break;

                        default:
                            if (day_of_week != dateTime.DayOfWeek)
                            {
                                day_of_week = dateTime.DayOfWeek;
                                Weather_next.Add(new Weather(
                                (string)item["main"]["temp"],
                                (string)item["main"]["temp_min"],
                                (string)item["main"]["temp_max"],
                                (string)item["weather"][0]["description"],
                                day_of_week.ToString()
                            ));
                            }
                            break;
                    }

                    index++;

                    Debug.WriteLine(dateTime.ToString());
                }

                index = 0;

                foreach (Weather Weather_tmp in Weather_next)
                {
                    if (index ==  3)
                    {
                        break;
                    }



                    index++;
                }

            }
            catch
            {
                // Details in ex.Message and ex.HResult.       
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();
        }

    }

    public class Weather : ObservableObject
    {

        public Weather(String Temp, String Temp_min, String Temp_max, String Description, String Day_week)
        {
            this.Temp = Temp;
            this.Temp_min = Temp_min;
            this.Temp_max = Temp_max;
            this.Description = Description;
            this.Day_week = Day_week;
        }

        String _temp = "";
        public String Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged("Temp");
            }
        }

        String _temp_min = "";
        public String Temp_min
        {
            get { return _temp_min; }
            set
            {
                _temp_min = value;
                OnPropertyChanged("Temp_min");
            }
        }

        String _temp_max = "";
        public String Temp_max
        {
            get { return _temp_max; }
            set
            {
                _temp_max = value;
                OnPropertyChanged("Temp_max");
            }
        }

        String _description = "";
        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        String _day_week = "";
        public String Day_week
        {
            get { return _day_week; }
            set
            {
                _day_week = value;
                OnPropertyChanged("Day_week");
            }
        }
    }
}
