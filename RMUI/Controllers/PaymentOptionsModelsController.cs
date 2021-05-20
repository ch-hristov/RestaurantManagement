using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RMUI.Data;
using RMUI.Models;

namespace RMUI.Views
{
    public class PaymentOptionsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentOptionsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentOptionsModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentOptionsModel.ToListAsync());
        }

        // GET: PaymentOptionsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOptionsModel = await _context.PaymentOptionsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentOptionsModel == null)
            {
                return NotFound();
            }

            return View(paymentOptionsModel);
        }

        // GET: PaymentOptionsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentOptionsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyPayments,Cash,CreditCards,VisaCards,MasterCardCards,MaestroCards,DinersCards,AmericanExpressCards,ApplePay")] PaymentOptionsModel paymentOptionsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentOptionsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentOptionsModel);
        }

        // GET: PaymentOptionsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOptionsModel = await _context.PaymentOptionsModel.FindAsync(id);
            if (paymentOptionsModel == null)
            {
                return NotFound();
            }
            return View(paymentOptionsModel);
        }

        // POST: PaymentOptionsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyPayments,Cash,CreditCards,VisaCards,MasterCardCards,MaestroCards,DinersCards,AmericanExpressCards,ApplePay")] PaymentOptionsModel paymentOptionsModel)
        {
            if (id != paymentOptionsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentOptionsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentOptionsModelExists(paymentOptionsModel.Id))
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
            return View(paymentOptionsModel);
        }

        // GET: PaymentOptionsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentOptionsModel = await _context.PaymentOptionsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentOptionsModel == null)
            {
                return NotFound();
            }

            return View(paymentOptionsModel);
        }

        // POST: PaymentOptionsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentOptionsModel = await _context.PaymentOptionsModel.FindAsync(id);
            _context.PaymentOptionsModel.Remove(paymentOptionsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentOptionsModelExists(int id)
        {
            return _context.PaymentOptionsModel.Any(e => e.Id == id);
        }
    }
}
