using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Lab5.Labs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lab5.Controllers
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
            ViewData["username"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals(JwtClaimTypes.Name))?.Value;
            ViewData["fullname"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals("fullname"))?.Value;
            ViewData["email"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals(JwtClaimTypes.Email))?.Value;
            ViewData["phone"] = User.Claims.FirstOrDefault(obj => obj.Type.Equals(JwtClaimTypes.PhoneNumber))?.Value;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
