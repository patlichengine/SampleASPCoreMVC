using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vereyon.Web;

namespace DemoApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlashMessage _flashMessage;

        public HomeController(ILogger<HomeController> logger, IFlashMessage flashMessage)
        {
            _logger = logger;
            _flashMessage = flashMessage ?? throw new ArgumentNullException(nameof(flashMessage));
        }

        public IActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            string emailAddress = identity.Claims.FirstOrDefault(c => c.Type.Contains("claims/name") && !c.Type.Contains("claims/nameident"))?.Value;
            
            _flashMessage.Info($"Welcome Dear {emailAddress}", "Login Successful");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
