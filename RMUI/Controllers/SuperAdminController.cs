using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMUI.Models;
using System.Threading.Tasks;

namespace RMUI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SuperAdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
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
                DoNotAllowToBecomeAdmin  = await _userManager.IsInRoleAsync(data, "DoNotAllowToBecomeAdmin"),
                DoNotAllowToBecomeManager = await _userManager.IsInRoleAsync(data, "DoNotAllowToBecomeManager"),
                DoNotAllowToBecomeServer = await _userManager.IsInRoleAsync(data, "DoNotAllowToBecomeServer"),
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditPerson(PersonDisplayModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (model.DoNotAllowToBecomeAdmin) await _userManager.AddToRoleAsync(user, "DoNotAllowToBecomeAdmin");
            else await _userManager.RemoveFromRoleAsync(user, "DoNotAllowToBecomeAdmin");

            if (model.DoNotAllowToBecomeManager) await _userManager.AddToRoleAsync(user, "DoNotAllowToBecomeManager");
            else await _userManager.RemoveFromRoleAsync(user, "DoNotAllowToBecomeManager");

            if (model.DoNotAllowToBecomeServer) await _userManager.AddToRoleAsync(user, "DoNotAllowToBecomeServer");
            else await _userManager.RemoveFromRoleAsync(user, "DoNotAllowToBecomeServer");


            if (model.IsAdmin) await _userManager.AddToRoleAsync(user, "Admin");
            else await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (model.IsWaiter) await _userManager.AddToRoleAsync(user, "Server");
            else await _userManager.RemoveFromRoleAsync(user, "Server");

            if (model.IsManager) await _userManager.AddToRoleAsync(user, "Manager");
            else await _userManager.RemoveFromRoleAsync(user, "Manager");

            return View(nameof(ViewPersons));
        }
    }
}