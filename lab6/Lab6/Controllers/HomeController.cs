using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Lab6.Labs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Lab6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var jsonToken = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();

            client.SetBearerToken(jsonToken);

            ViewBag.Test = await client.GetStringAsync("http://localhost:4000/api/test/gettext");
            return View();
        }

        [Authorize]
        public IActionResult Lab(int number)
        {
            ViewData["Lab"] = number;
            ILab lab;
            switch (number)
            {
                case 1:
                    lab = new Lab1();
                    break;
                case 2:
                    lab = new Lab2();
                    break;
                case 3:
                    lab = new Lab3();
                    break;
                default:
                    return Error();
            }
            ViewData["description"] = lab.Description;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lab(int number, string input)
        {
            ViewData["Lab"] = number;
            ILab lab;
            switch (number)
            {
                case 1:
                    lab = new Lab1();
                    break;
                case 2:
                    lab = new Lab2();
                    break;
                case 3:
                    lab = new Lab3();
                    break;
                default:
                    return Error();
            }
            ViewData["description"] = lab.Description;
            ViewData["output"] = lab.Execute(input);
            return View();
        }
        [Authorize]
        public IActionResult UserPage()
        {
            ViewData["username"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals("username"))?.Value;
            ViewData["fullname"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals("fullname"))?.Value;
            ViewData["email"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals("email_address"))?.Value;
            ViewData["phone"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals("phone"))?.Value;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
