using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWebApp.Models
{
    public class Location
    {
        public string Title { get; set; }
        public string Location_Type { get; set; }
        public string Latt_Long { get; set; }
        public int WoeId { get; set; }
        public int Distance { get; set; }
    }
}
