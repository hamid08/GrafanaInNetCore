using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Prometheus;

namespace GrafanaInNetCore.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        Counter counter = Metrics
           .CreateCounter("GetCityError", "این متد خطا های دریافت شهرها را نمایش می دهد");

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Task.Delay(5000);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("city")]
        public ActionResult<string> City()
        {
            try
            {
              throw new ArgumentException("Parameter cannot be null");
                //Thread.Sleep(TimeSpan.FromSeconds(5));
                //return "Melbourne";
            }
            catch (Exception)
            {
                counter.Inc();
                return null;
            }
        }

        [HttpGet("country")]
        public ActionResult<string> Country()
        {
            var rng = new Random().Next(1, 10);

            if (rng > 5)
            {
                return Unauthorized();
            }
            return "Australia";
        }
    }
}