using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class WeatherResponse
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather? Current { get; set; }
    }
}
