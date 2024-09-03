using Demo_FB_LINK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Demo_FB_LINK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Demo()
        {
            return View();
        }

        public async Task<IActionResult> URLLinkedin(string state, string code)
        {

            if(string.IsNullOrEmpty(code))
                return View();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.linkedin.com/oauth/v2/accessToken");
            request.Headers.Add("Cookie", "bcookie=\"v=2&13b807d8-0630-46c5-8ca1-9b7b810f9642\"; lang=v=2&lang=en-us; lidc=\"b=OB46:s=O:r=O:a=O:p=O:g=5061:u=38:x=1:i=1725374226:t=1725456526:v=2:sig=AQGb1j8V7HBdcfVe0kuKtJchWxd_Gczc\"; JSESSIONID=ajax:1138037632392933823; PLAY_SESSION=eyJhbGciOiJIUzI1NiJ9.eyJkYXRhIjp7ImZsb3dUcmFja2luZ0lkIjoiZzJOQXlpZDdUMWlqODQrWEFOMlUxdz09In0sIm5iZiI6MTcyNTM3MTkxMiwiaWF0IjoxNzI1MzcxOTEyfQ.LVTYM3a6dlqxovtHjOnKXiHYS4Iqe73cX7MOI_zTq4A; bscookie=\"v=1&202409031349118756c144-64e7-4914-88fb-8319a263c228AQHKDaHUNvS3YX4Z3JLSB4hZD2tY63m5\"");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("grant_type", "authorization_code"));
            collection.Add(new("code", code));
            collection.Add(new("client_id", "86rpnm3kvlpu2a"));
            collection.Add(new("client_secret", "xfBh0Ncdk2KsdnNJ"));
            collection.Add(new("redirect_uri", "https://localhost:6100/Home/URLLinkedin"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseBody);
            var accessToken = jsonDocument.RootElement.GetProperty("access_token").GetString();
            ViewData["accessToken"] = accessToken;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
