using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Class1 _class1;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Class1 class1)
        {
            _logger = logger;
            _class1 = class1;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public WeatherForecast GetById(int id)
        {
            
            var rng = new Random();
            //var msg = $"GetById -> Some bad code was executed!";
            //throw new Exception(msg);

            //return new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(id),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[id]
            //};
            _class1.Calc(0);

            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[id]
            };

        }
    }
}
