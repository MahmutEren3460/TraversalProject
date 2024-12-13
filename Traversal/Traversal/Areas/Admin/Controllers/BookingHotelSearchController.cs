using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Traversal.Areas.Admin.Models;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingHotelSearchController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var destId = TempData["CityID"] as string;
            if (destId == null)
            {
                return RedirectToAction("GetCityID");
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v2/hotels/search?dest_id={destId}&order_by=popularity&checkout_date=2025-01-19&children_number=2&filter_by_currency=EUR&locale=en-gb&dest_type=city&checkin_date=2025-01-18&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&children_ages=5%2C0&include_adjacency=true&page_number=0&adults_number=2&room_number=1&units=metric"),
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
                var bodyReplace = body.Replace(".", "");
                var values = JsonConvert.DeserializeObject<BookingHotelSearchViewModel>(bodyReplace);
                ViewBag.CityName = TempData["CityName"];
                return View(values.results);
            }
        }


        [HttpGet]
        public IActionResult GetCityID()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCityID(string p)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?locale=en-gb&name={p}"),
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
                var locations = JsonConvert.DeserializeObject<List<BookingHotelSearchViewModel2.Class1>>(body);
                var destId = locations?.FirstOrDefault()?.dest_id;
                if (destId != null)
                {
                    TempData["CityID"] = destId;
                    TempData["CityName"] = locations.FirstOrDefault()?.name;
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

    }
}
