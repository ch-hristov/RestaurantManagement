using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMUI.Models;

namespace RMUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        // Run once to insert roles for user
        //public async Task<IActionResult> Privacy()
        //{


        //    var user1 = await _userManager.FindByEmailAsync("yimiao@iamyimiao.com");
        //    if (user1 != null)
        //    {
        //        await _userManager.AddToRoleAsync(user1, "Admin");
        //        await _userManager.AddToRoleAsync(user1, "Manager");
        //        await _userManager.AddToRoleAsync(user1, "Chef");
        //    }

        //    var user2 = await _userManager.FindByEmailAsync("amy@iamamy.com");
        //    if (user2 != null)
        //    {
        //        await _userManager.AddToRoleAsync(user2, "Manager");
        //        await _userManager.AddToRoleAsync(user2, "Server");
        //    }
        //    return View();
        //}


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
