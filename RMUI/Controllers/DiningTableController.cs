using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "SuperAdmin,Manager,Admin")]
    public class DiningTableController : Controller
    {
        private readonly IDiningTableData _data;

        public DiningTableController(IDiningTableData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> InsertDiningTable(DiningTableDisplayModel table)
        {
            if (ModelState.IsValid)
            {
                var newTable = new DiningTableModel
                {
                    TableNumber = table.TableNumber,
                    Seats = 4,
                    IsBlocked = false
                };

                await _data.InsertTable(newTable);

                return RedirectToAction("ViewDiningTables");
            }

            return View();
        }

        public async Task<IActionResult> BlockDiningTable(int id, bool status)
        {
            var foundTable = await _data.GetTableById(id);

            foundTable.IsBlocked = status;

            await _data.UpdateTable(foundTable);

            return RedirectToAction(nameof(ViewDiningTables));

        }


        // View all DiningTables as a list
        public async Task<IActionResult> ViewDiningTables()
        {
            var allTables = await _data.GetAllTables();

            var tables = new List<DiningTableDisplayModel>();

            foreach (var table in allTables)
            {
                if (table.IsHidden) continue;
                tables.Add(new DiningTableDisplayModel
                {
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    IsBlocked = table.IsBlocked
                });
            }

            return View(tables);
        }


        // Edit DiningTable with database Id = id
        public async Task<IActionResult> EditDiningTable(int id)
        {
            DiningTableModel foundTable = await _data.GetTableById(id);

            DiningTableDisplayModel table = new DiningTableDisplayModel
            {
                Id = foundTable.Id,
                TableNumber = foundTable.TableNumber,
            };

            return View(table);
        }


        // Update DiningTable info
        public async Task<IActionResult> UpdateDiningTable(DiningTableDisplayModel table)
        {
            DiningTableModel updatedTable = new DiningTableModel
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
            };

            await _data.UpdateTable(updatedTable);

            return RedirectToAction("ViewDiningTables");
        }


        // Delete DiningTable with database Id = id
        public async Task<IActionResult> DeleteDiningTable(int id)
        {
            var table = await _data.GetTableById(id);
            if (table != null)
            {
                table.IsHidden = true;
                await _data.UpdateTable(table);
            }
            return RedirectToAction("ViewDiningTables");
        }
    }
}