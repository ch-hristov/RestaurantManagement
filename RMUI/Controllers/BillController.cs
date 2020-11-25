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
    public class BillController : Controller
    {
        private readonly IOrderData _order;
        private readonly IDiningTableData _table;
        private readonly IPersonData _people;
        private readonly IFoodData _food;
        private readonly IBillData _bill;

        public BillController(IOrderData order, IDiningTableData table, IPersonData people, IFoodData food, IBillData bill)
        {
            _order = order;
            _table = table;
            _people = people;
            _food = food;
            _bill = bill;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Get bill details for a specific dining table with TableNumber = tableNumber
        [HttpPost]
        public async Task<IActionResult> ViewBill(int tableNumber)
        {
            if (await _table.IsValidTableNumber(tableNumber) == false)
            {
                return RedirectToAction("TableNotExistError", "Home");
            }
            var table = await _table.GetTableByTableNumber(tableNumber);
            var order = await _order.GetOrderByTable(table.Id);
            if (order == null)
            {
                return RedirectToAction("NoOrderError", "Home");
            }

            var server = await _people.GetPersonById(order.ServerId);
            var orderDetails = await _order.GetOrderDetailByDiningTable(table.Id);

            BillDisplayModel bill = new BillDisplayModel
            {
                OrderId = order.Id,
                TableNumber = tableNumber,
                Server = server.FullName,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Total = order.Total,
                BillPaid = order.BillPaid
            };

            if (orderDetails == null)
            {
                return RedirectToAction("NoOrderError", "Home");
            }
            else
            {
                foreach (var detail in orderDetails)
                {
                    var food = await _food.GetFoodById(detail.FoodId);

                    bill.OrderDetails.Add(new OrderDetailDisplayModel
                    {
                        Id = detail.Id,
                        TableNumber = table.TableNumber,
                        Server = server.FullName,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate
                    });
                }
            }

            return View(bill);
        }


        // Search for bill details by typing in table number in view
        public IActionResult SearchBill()
        {
            return View();
        }


        // Pay Bill button press will insert bill info into database and update BillPaid info
        // in order table and orderDetail table
        public async Task<IActionResult> PayBill(BillDisplayModel displayBill)
        {
            var table = await _table.GetTableByTableNumber(displayBill.TableNumber);
            string[] fullName = displayBill.Server.Split(" ");
            var server = await _people.GetPersonByFullName(fullName[0], fullName[1]);

            BillModel bill = new BillModel
            {
                OrderId = displayBill.OrderId,
                DiningTableId = table.Id,
                ServerId = server.Id,
                SubTotal = displayBill.SubTotal,
                Tax = displayBill.Tax,
                Total = displayBill.Total,
                BillPaid = true
            };

            await _bill.InsertBill(bill);

            return RedirectToAction("Index", "Home");
        }
    }
}