using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustEat.StatsD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace StatsWeb.Api.Controllers
{
    public static class WebMetrics
    {
        private static readonly Counter Counter =
            Metrics.CreateCounter("weather_request_total", "Total number of requests");
        
        public static void WeatherCalled()
        {
            Counter.Inc();
        }
    }
    
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStatsDPublisher _statsPublisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStatsDPublisher statsPublisher)
        {
            _logger = logger;
            _statsPublisher = statsPublisher;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            WebMetrics.WeatherCalled();
            _statsPublisher.Increment("weather_request_total");
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}