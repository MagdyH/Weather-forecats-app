using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWebApp.Models
{
    public class WeatherResopnse
    {
        public List<WeatherForecast> Consolidated_Weather { get; set; }
        public DateTime Time { get; set; }
        public DateTime Sun_Rise { get; set; }
        public DateTime Sun_Set { get; set; }
        public string Timezone_Name { get; set; }
        public string Title { get; set; }
        public string Location_Type { get; set; }
    }
}
