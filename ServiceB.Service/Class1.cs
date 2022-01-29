using Microsoft.Extensions.Logging;
using System;

namespace ServiceB.Service
{
    public class Class1
    {
        private readonly ILogger _logger;
        public Class1(ILogger<Class1> logger)
        {
            _logger = logger;
        }

        public void Calc(int id)
        {
            _logger.LogInformation("Test Value : " + id);
            var rng = new Random();
            var msg = $"GetById -> Some bad code was executed!";
            throw new Exception(msg);
        }
    }
}
