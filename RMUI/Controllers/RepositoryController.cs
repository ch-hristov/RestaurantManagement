using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class RepositoryController : Controller
    {
        private readonly IOrderData _order;
        private readonly IPersonData _people;
        private readonly IFoodData _food;
        private readonly IDiningTableData _table;
        private readonly UserManager<IdentityUser> _mng;

        public RepositoryController(IOrderData order, IDiningTableData table, IPersonData people, IFoodData food, UserManager<IdentityUser> mng)
        {
            _order = order;
            _people = people;
            _food = food;
            _table = table;
            _mng = mng;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await _order.GetAllOrderRecords();

            List<OrderDisplayModel> orders = new List<OrderDisplayModel>();
            foreach (var order in allOrders)
            {
                var table = await _table.GetTableById(order.DiningTableId);
                var server = await _mng.FindByIdAsync(order.ServerId);

                orders.Add(new OrderDisplayModel
                {
                    Id = order.Id,
                    TableNumber = table.TableNumber,
                    Server = server.Email,
                    SubTotal = order.SubTotal,
                    Tax = order.Tax,
                    Total = order.Total,
                    CreatedDate = order.CreatedDate,
                    BillPaid = order.BillPaid
                });
            }

            return View(orders);
        }


        public async Task<IActionResult> GetAllOrderDetails()
        {
            var allDetails = await _order.GetAllOrderDetailRecords();

            List<OrderDetailDisplayModel> details = new List<OrderDetailDisplayModel>();
            foreach (var detail in allDetails)
            {
                var table = await _table.GetTableById(detail.DiningTableId);
                var server = await _mng.FindByIdAsync(detail.ServerId);
                var food = await _food.GetFoodById(detail.FoodId);

                details.Add(new OrderDetailDisplayModel
                {
                    Id = detail.Id,
                    TableNumber = table.TableNumber,
                    Server = server.Email,
                    FoodName = food.FoodName,
                    Price = food.Price,
                    Quantity = detail.Quantity,
                    OrderDate = detail.OrderDate,
                    OrderId = detail.OrderId
                });
            }

            return View(details);
        }
    }
}