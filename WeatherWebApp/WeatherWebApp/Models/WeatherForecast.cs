using Microsoft.AspNetCore.Authentication;
using System;

namespace WeatherWebApp
{
    public class WeatherForecast
    {
        public long Id { get; set; }
        public string Weather_State_Name { get; set; }
        public string Weather_State_Abbr { get; set; }
        public string Wind_Direction_Compass { get; set; }
        public DateTime Created { get; set; }
        public DateTime Applicable_Date { get; set; }        
        public float Min_Temp { get; set; }
        public float Max_Temp { get; set; }
        public float The_Temp { get; set; }
        public float Wind_Speed { get; set; }
        public float Wind_Direction { get; set; }
        public float Air_Pressure { get; set; }
        public float Humidity { get; set; }
        public float Visibility { get; set; }
        public int Predictability { get; set; }
        public string Image_Url { get { return "https://www.metaweather.com/static/img/weather/png/" + this.Weather_State_Abbr + ".png";  } }

    }
}
