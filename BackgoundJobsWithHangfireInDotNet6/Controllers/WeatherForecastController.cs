using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgoundJobsWithHangfireInDotNet6.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //BackgroundJob.Enqueue(() => SendMessage("elhelaly@outlook.com"));

            //Console.WriteLine(DateTime.Now);
            //BackgroundJob.Schedule(() => SendMessage("elhelaly@outlook.com"), TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate(() => SendMessage("elhelaly@outlook.com"), Cron.Minutely);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMessage(string email)
        {
            Console.WriteLine($"Email sent at {DateTime.Now}");
        }
    }
}