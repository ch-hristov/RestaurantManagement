using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RMUI.Models;

namespace RMUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(
                                ILogger<HomeController> logger, 
                                UserManager<IdentityUser> userManager, 
                                RoleManager<IdentityRole> roleManager,
                                IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            ViewBag.StatisticsUrl = "https://MAika.com";
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
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


        public IActionResult PlaceOrderError()
        {
            return View();
        }


        public IActionResult NoOrderError()
        {
            return View();
        }


        public IActionResult TableNotExistError()
        {
            return View();
        }
    }
}
