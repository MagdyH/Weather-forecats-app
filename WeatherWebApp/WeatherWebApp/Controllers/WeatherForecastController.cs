using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherWebApp.Middlewares;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers
{
    [ApiController]
    [ApiKeyAuth]
    [Produces("application/json")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            var weatherResopnse = new WeatherResopnse();
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get,
                    "https://www.metaweather.com/api/location/search/?query=Belfast");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject
                    <IEnumerable<Location>>(responseStream);

                if (data.Count() > 0)
                {
                    request = new HttpRequestMessage(HttpMethod.Get,
                        "https://www.metaweather.com/api/location/" + data.FirstOrDefault().WoeId + "/");

                    response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        responseStream = await response.Content.ReadAsStringAsync();
                        weatherResopnse = JsonConvert.DeserializeObject
                            <WeatherResopnse>(responseStream);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                return BadRequest();
            }

            var weatherForecasts = weatherResopnse.Consolidated_Weather.Where(forecast => forecast.Applicable_Date > DateTime.Today).ToList();
            return Ok(weatherForecasts);
        }
    }
}
