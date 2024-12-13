using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Traversal.Areas.Admin.Models;

namespace Traversal.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ApiExchangeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<BookingExchangeViewModel2> list = new List<BookingExchangeViewModel2>();
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?locale=en-gb&currency=TRY"),
				Headers =
	{
		{ "x-rapidapi-key", "3b03f350f1msh02d1fff7780ec69p17237djsn8ca9d8755a04" },
		{ "x-rapidapi-host", "booking-com.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<BookingExchangeViewModel2>(body);
                return View(values.exchange_rates);
            }
			
		}
	}
}
