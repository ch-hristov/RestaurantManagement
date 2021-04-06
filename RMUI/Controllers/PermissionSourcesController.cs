using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RMUI.Data;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PermissionSourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionSourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PermissionSource.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionSource = await _context.PermissionSource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissionSource == null)
            {
                return NotFound();
            }

            return View(permissionSource);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CanWorkWithTables,CanWorkWithOrders,CanWorkWithAds,CanWorkWithFoods,CanPeopleRegister")] PermissionSource permissionSource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissionSource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissionSource);
        }

        // GET: PermissionSources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionSource = await _context.PermissionSource.FindAsync(id);
            if (permissionSource == null)
            {
                return NotFound();
            }
            return View(permissionSource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CanWorkWithTables,CanWorkWithOrders,CanWorkWithAds,CanWorkWithFoods,CanPeopleRegister")] PermissionSource permissionSource)
        {
            if (id != permissionSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionSource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionSourceExists(permissionSource.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permissionSource);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionSource = await _context.PermissionSource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permissionSource == null)
            {
                return NotFound();
            }

            return View(permissionSource);
        }

        // POST: PermissionSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissionSource = await _context.PermissionSource.FindAsync(id);
            _context.PermissionSource.Remove(permissionSource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionSourceExists(int id)
        {
            return _context.PermissionSource.Any(e => e.Id == id);
        }
    }
}
