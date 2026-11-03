using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BLL.Services
{
    public class WeatherService
    {
        private HttpClient http;

        public WeatherService(HttpClient httpClient)
        {
            http=httpClient;
        }

        public async Task<CurrentWeather?> GetAsync(
        double lat, double lon, CancellationToken ct = default)
        {
            var url = $"v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

            var response = await http.GetFromJsonAsync<WeatherResponse>(url, ct);
            return response?.Current;
        }
    }
}
