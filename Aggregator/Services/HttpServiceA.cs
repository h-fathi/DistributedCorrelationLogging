using Aggregator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
    public class HttpServiceA : IServiceA
    {
        private readonly HttpClient _client;

        public HttpServiceA(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }


        public async Task<WeatherForecastModel> GetWeatherForecast(int id)
        {
            var response = await _client.GetAsync($"/WeatherForecast/{id}");
            return await response.ReadContentAs<WeatherForecastModel>();
        }
    }
}
