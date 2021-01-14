using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMUI.Models;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IStringLocalizer<BillController> _localizer;

        public BillController(IOrderData order,
                              IDiningTableData table,
                              IPersonData people,
                              IFoodData food,
                              IBillData bill,
                              IStringLocalizer<BillController> localizer)
        {
            _order = order;
            _table = table;
            _people = people;
            _food = food;
            _bill = bill;
            _localizer = localizer;
        }

        public IActionResult Index()
        {

            return View();
        }


        // Get bill details for a specific dining table with TableNumber = tableNumber
        [HttpGet]
        public async Task<IActionResult> ViewBill(int tableNumber)
        {
            ViewBag.FoodNameLabel = _localizer["Food Name"];
            ViewBag.FoodPriceLabel = _localizer["Food Price"];
            ViewBag.FoodQuantityLabel = _localizer["Items Quantity"];
            ViewBag.FoodOrderDateLabel = _localizer["Order Date"];
            ViewBag.BillDetailLabel = _localizer["Bill Detail"];
            ViewBag.TableNumberLabel = _localizer["Table Number"];
            ViewBag.Server = _localizer["Server"];
            ViewBag.Subtotal = _localizer["Subtotal"];
            ViewBag.Tax = _localizer["Tax"];
            ViewBag.Total = _localizer["Total"];
            ViewBag.BillPaid = _localizer["Bill Paid"];
            ViewBag.PayBill = _localizer["Pay Bill"];

            if (await _table.IsValidTableNumber(tableNumber) == false)
                return RedirectToAction("TableNotExistError", "Home");


            var table = await _table.GetTableByTableNumber(tableNumber);
            var orderDetails = await _order.GetOrderDetailByDiningTable(table.Id);

            if (orderDetails.Count == 0)
                return RedirectToAction("NoOrderError", "Home");

            var serverId = orderDetails.FirstOrDefault().ServerId;
            var server = await _people.GetPersonById(serverId);

            // computes total amounts
            var orderDisplay = await OrderDisplayModel.FromDetails(orderDetails, _food, 0, server.FullName, table.Id);

            var bill = new BillDisplayModel
            {
                TableNumber = tableNumber,
                Server = server.FullName,
                SubTotal = orderDisplay.SubTotal,
                Tax = orderDisplay.Tax,
                Total = orderDisplay.Total,
                BillPaid = false
            };

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
            await _order.InsertOrderByTable(table.Id);

            var newOrder = await _order.GetOrderByTable(table.Id);
            await _order.PayBill(newOrder);

            return RedirectToAction("Index", "Home");
        }
    }
}