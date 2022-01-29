using Aggregator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
    public class HttpServiceB : IServiceB
    {
        private readonly HttpClient _client;

        public HttpServiceB(HttpClient client)
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
