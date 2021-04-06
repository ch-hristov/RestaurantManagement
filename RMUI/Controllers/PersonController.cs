using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PersonController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PersonController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> ViewPersons()
        {
            var roles = await _userManager.Users.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> EditPerson(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            return View(new PersonDisplayModel()
            {
                EmailAddress = data.Email,
                Id = data.Id,
                IsAdmin = await _userManager.IsInRoleAsync(data, "Admin"),
                IsManager = await _userManager.IsInRoleAsync(data, "Manager"),
                IsWaiter = await _userManager.IsInRoleAsync(data, "Server"),
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditPerson(PersonDisplayModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            var restrictAdmin = await _userManager.IsInRoleAsync(user, "DoNotAllowToBecomeAdmin");
            var restrictMng = await _userManager.IsInRoleAsync(user, "DoNotAllowToBecomeManager");
            var restrictWaiter = await _userManager.IsInRoleAsync(user, "DoNotAllowToBecomeServer");

            if (model.IsAdmin && !restrictAdmin) await _userManager.AddToRoleAsync(user, "Admin");
            else
            {
                if (!restrictAdmin)
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
            }

            if (model.IsWaiter && !restrictWaiter)
            {

                await _userManager.AddToRoleAsync(user, "Server");
            }
            else
            {
                if (!restrictWaiter)
                    await _userManager.RemoveFromRoleAsync(user, "Server");
            }

            if (model.IsManager && !restrictMng) await _userManager.AddToRoleAsync(user, "Manager");
            else
            {
                if (!restrictMng)
                    await _userManager.RemoveFromRoleAsync(user, "Manager");
            }

            return View(nameof(ViewPersons));
        }
    }
}