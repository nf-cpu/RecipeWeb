using BLL.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private WeatherService weatherService;

        public WeatherController(WeatherService weatherService)
        {
            this.weatherService=weatherService;
        }

        [HttpGet]
        public async Task<ActionResult<CurrentWeather>> Get(
            double lat, double lon, CancellationToken ct)
        {
            var result = await weatherService.GetAsync(lat, lon, ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
