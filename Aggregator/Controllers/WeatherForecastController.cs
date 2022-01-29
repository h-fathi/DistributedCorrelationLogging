using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IServiceA _serviceA;
        private readonly IServiceB _serviceB;

        public WeatherForecastController(IServiceA serviceA, IServiceB serviceB, ILogger<WeatherForecastController> logger)
        {
            _serviceA = serviceA ?? throw new ArgumentNullException(nameof(serviceA));
            _serviceB = serviceB ?? throw new ArgumentNullException(nameof(serviceB));
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAll()
        {

            _logger.LogInformation("Start request to GetAll");
            var firstForecast = await _serviceA.GetWeatherForecast(1);
                var lastForecast = await _serviceB.GetWeatherForecast(2);
            _logger.LogInformation("End request to GetAll");
            return Ok( new List<WeatherForecastModel>() { firstForecast, lastForecast });


        }
    }
}
