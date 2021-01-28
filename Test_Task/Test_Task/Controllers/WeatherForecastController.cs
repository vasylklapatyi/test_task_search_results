using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Test_Task.Controllers
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
			string html = "";
			CQ cq = CQ.Create(html);
			foreach (IDomObject obj in cq.Find("a"))
				Console.WriteLine(obj.GetAttribute("href"));
		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => "test");
		}
	}
}
