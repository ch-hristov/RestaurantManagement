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
    [Authorize(Roles = "Manager")]
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

        // Insert DiningTable into database
        public async Task<IActionResult> InsertDiningTable(DiningTableDisplayModel table)
        {
            if (ModelState.IsValid)
            {
                DiningTableModel newTable = new DiningTableModel
                {
                    TableNumber = table.TableNumber,
                    Seats = table.Seats
                };

                await _data.InsertTable(newTable);

                return RedirectToAction("ViewDiningTables");
            }

            return View();
        }


        // View all DiningTables as a list
        public async Task<IActionResult> ViewDiningTables()
        {
            var allTables = await _data.GetAllTables();

            List<DiningTableDisplayModel> tables = new List<DiningTableDisplayModel>();

            foreach (var table in allTables)
            {
                tables.Add(new DiningTableDisplayModel
                {
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    Seats = table.Seats
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
                Seats = foundTable.Seats
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
                Seats = table.Seats
            };

            await _data.UpdateTable(updatedTable);

            return RedirectToAction("ViewDiningTables");
        }


        // Delete DiningTable with database Id = id
        public async Task<IActionResult> DeleteDiningTable(int id)
        {
            await _data.DeleteTable(id);

            return RedirectToAction("ViewDiningTables");
        }
    }
}