using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Server,Manager,Admin,SuperAdmin")]
    public partial class OrderController : Controller
    {
        private readonly IFoodData _food;
        private readonly IPersonData _people;
        private readonly IDiningTableData _table;
        private readonly IOrderData _order;
        private readonly IStringLocalizer<OrderController> _localizer;
        private readonly UserManager<IdentityUser> _userMng;

        public OrderController(IFoodData food, IPersonData people,
                               IDiningTableData table, IOrderData order,
                               IStringLocalizer<OrderController> localizer,
                               UserManager<IdentityUser> userMng)
        {
            _food = food;
            _people = people;
            _table = table;
            _order = order;
            _localizer = localizer;
            _userMng = userMng;
        }


        // Get all Dining Tables and populate as dropdown list items
        public async Task<List<SelectListItem>> GetAllTables()
        {
            var tables = await _table.GetAllTables();
            var list = new List<SelectListItem>();

            foreach (var table in tables)
            {
                list.Add(new SelectListItem
                {
                    Text = table.TableNumber.ToString(),
                    Value = table.Id.ToString()
                });
            }

            return list;
        }


        // Get all Servers and populate as dropdown list items
        public async Task<List<SelectListItem>> GetAllServers()
        {
            var user = await _userMng.GetUserAsync(HttpContext.User);
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem
            {
                Text = user.Email,
                Value = user.Id.ToString()
            });

            return list;
        }


        // Get all Food Types and populate as dropdown list items
        public async Task<List<SelectListItem>> GetFoodTypes()
        {
            var foodTypes = await _food.GetAllFoodTypes();
            var list = new List<SelectListItem>();

            foreach (var foodType in foodTypes)
            {
                list.Add(new SelectListItem
                {
                    Text = foodType.FoodType,
                    Value = foodType.Id.ToString()
                });
            }

            return list;
        }

        // Get all foods with FoodType Id = typeId and populate as dropdown list items
        // return as JsonResult for use in JQuery function call
        public async Task<JsonResult> GetFoodsByTypeId(int typeId)
        {
            var foods = await _food.GetFoodByTypeId(typeId);
            var list = new List<SelectListItem>();

            foreach (var food in foods)
            {
                list.Add(new SelectListItem
                {
                    Text = food.FoodName,
                    Value = food.Id.ToString()
                });
            }

            return Json(list);
        }

        public async Task<IActionResult> Statistics(StatisticTarget statistic = StatisticTarget.Week)
        {
            var orders = await _order.GetAllOrderDetailRecords();
            var days = 0;

            switch (statistic)
            {
                case StatisticTarget.Day:
                    days = 1;
                    break;
                case StatisticTarget.Week:
                    days = 7;
                    break;
                case StatisticTarget.Month:
                    days = 30;
                    break;
                case StatisticTarget.Year:
                    days = 365;
                    break;
                default:
                    break;
            }

            var lowerBoundary = DateTime.Now.Date - new TimeSpan(days, 0, 0, 0);

            var requests = orders.Where(x => x.OrderDate.Date >= lowerBoundary)
                                 .GroupBy(x => x.FoodId)
                                 .Select((group) =>
                                {
                                    return new OrderStatistic()
                                    {
                                        FoodName = _food.GetFoodById(group.Key).Result,
                                        Amount = group.Sum(x => x.Quantity)
                                    };
                                })
                                 .OrderByDescending(b => b.Amount)
                                 .ToList();

            return View(requests);
        }


        // Get Food object with Id = id and return as JsonResult for use in JQuery function call
        public async Task<JsonResult> GetFoodById(int id)
        {
            var food = await _food.GetFoodById(id);
            return Json(food);
        }


        // Populate the table number and server fullName dropdown lists and foodType cascading dropdown list
        public async Task<IActionResult> CreateOrder()
        {
            var tables = await GetAllTables();
            ViewBag.TableList = tables;

            var servers = await GetAllServers();
            ViewBag.ServerList = servers;

            var foods = await GetFoodTypes();
            ViewBag.FoodTypeList = foods;

            var list = new List<SelectListItem>();

            foreach (var food in new[] { "A", "B" , "C", "D" })
            {
                list.Add(new SelectListItem
                {
                    Text = food.ToString(),
                    Value = food.ToString()
                });
            }

            ViewBag.SeatList = list;

            ViewBag.CreateOrderLabel = _localizer["Create Order"];
            ViewBag.AddFoodLabel = _localizer["Add food"];

            return View();
        }

        // Post the input food order detail for inserting into databse and repopulate dropdown lists
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderFillInModel order)
        {
            if (ModelState.IsValid)
            {
                var t = await GetAllTables();
                var table = await _table.GetTableByTableNumber(int.Parse(t.ToList()[order.TableNumber - 1].Text));
                var detail = new OrderDetailModel
                {
                    DiningTableId = table.Id,
                    ServerId = order.Server,
                    FoodId = int.Parse(order.FoodName),
                    Quantity = order.Quantity,
                    SeatNumber = order.SeatNumber
                };

                await _order.InsertOrderDetail(detail);
            }

            // Dropdown list content is null after data post, need to repopulate the list
            var tables = await GetAllTables();
            ViewBag.TableList = tables;

            var servers = await GetAllServers();
            ViewBag.ServerList = servers;

            var foods = await GetFoodTypes();
            ViewBag.FoodTypeList = foods;
            var list = new List<SelectListItem>();

            foreach (var food in Enumerable.Range(1, 4))
            {
                list.Add(new SelectListItem
                {
                    Text = food.ToString(),
                    Value = food.ToString()
                });
            }

            ViewBag.SeatList = list;


            return View();
        }


        // Get the list of ordered food details by dining table TableNumber = tableNumber
        // Need to type in the tableNumber in view for search request 
        [HttpPost]
        public async Task<IActionResult> ViewOrderByTable(int tableNumber)
        {
            if (await _table.IsValidTableNumber(tableNumber) == false)
            {
                return RedirectToAction("TableNotExistError", "Home");
            }

            var table = await _table.GetTableByTableNumber(tableNumber);
            var displayDetails = new List<OrderDetailDisplayModel>();
            var orderDetails = await _order.GetOrderDetailByDiningTable(table.Id);

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("NoOrderError", "Home");
            }
            else
            {
                var server = await _userMng.FindByIdAsync(orderDetails[0].ServerId);

                foreach (var detail in orderDetails)
                {
                    var food = await _food.GetFoodById(detail.FoodId);

                    displayDetails.Add(new OrderDetailDisplayModel
                    {
                        Id = detail.Id,
                        TableNumber = tableNumber,
                        Server = server.Email,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate
                    });
                }
                return View(displayDetails);
            }
        }


        // Search for order summary by typing in table number in view
        public IActionResult SearchOrder()
        {
            return View();
        }


        // Get the order summary for records which haven't been paid just yet
        public async Task<IActionResult> ViewUnpaidRecordsAsOrders()
        {
            var orders = new List<OrderDisplayModel>();
                

            foreach (var table in await _table.GetAllTables())
            {
                var orderList = await _order.GetOrderDetailByDiningTable(table.Id);

                if (orderList.Count > 0)
                {

                    var firstOrder = orderList.OrderBy(x => x.OrderDate).First();

                    var server = await _userMng.FindByIdAsync(firstOrder.ServerId);
                    var orderDisplay = await OrderDisplayModel.FromDetails(orderList, _food, 0, server.Email , table.TableNumber);
                    orders.Add(orderDisplay);
                }
            }
            return View("ViewOrders", orders);
        }

        [HttpGet]
        // Get the order summary for all dining tables as a list
        public async Task<IActionResult> ViewOrderRecords()
        {
            var orderList = await _order.GetAllOrders();

            var orders = new List<OrderDisplayModel>();

            foreach (var order in orderList.OrderByDescending(x => x.CreatedDate))
            {
                var table = await _table.GetTableById(order.DiningTableId);
                var server = await _userMng.FindByIdAsync(order.ServerId);

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

            return View("ViewOrders", orders);
        }

        // Edit a specific ordered food detail with Id = id
        public async Task<IActionResult> EditOrderDetail(int id)
        {
            var detail = await _order.GetOrderDetailById(id);
            var table = await _table.GetTableById(detail.DiningTableId);
            var server = await _userMng.FindByIdAsync(detail.ServerId);
            var food = await _food.GetFoodById(detail.FoodId);

            OrderDetailDisplayModel displayDetail = new OrderDetailDisplayModel
            {
                Id = id,
                TableNumber = table.TableNumber,
                Server = server.Email,
                FoodName = food.FoodName,
                Price = food.Price,
                Quantity = detail.Quantity,
                OrderDate = detail.OrderDate,
            };

            return View(displayDetail);
        }

        // Get a specific ordered food detail with Id = id
        public async Task<IActionResult> OrderByTableDetail(int id)
        {
            var detail = await _order.GetOrderDetailById(id);
            var table = await _table.GetTableById(detail.DiningTableId);
            var server = await _userMng.FindByIdAsync(detail.ServerId);
            var food = await _food.GetFoodById(detail.FoodId);

            var displayDetail = new OrderDetailDisplayModel
            {
                Id = id,
                TableNumber = table.TableNumber,
                Server = server.Email,
                FoodName = food.FoodName,
                Price = food.Price,
                Quantity = detail.Quantity,
                OrderDate = detail.OrderDate
            };

            return View(displayDetail);
        }


        // Get the list of ordered food details by dining table TableNumber = tableNumber
        // Used for hyperlinks in views
        public async Task<IActionResult> OrderDetailsByTableNumber(int tableNumber)
        {
            var table = await _table.GetTableByTableNumber(tableNumber);
            var orderDetails = await _order.GetOrderDetailByDiningTable(table.Id);

            var displayDetails = new List<OrderDetailDisplayModel>();

            foreach (var detail in orderDetails)
            {
                var server = await _userMng.FindByIdAsync(detail.ServerId);
                var food = await _food.GetFoodById(detail.FoodId);

                displayDetails.Add(new OrderDetailDisplayModel
                {
                    Id = detail.Id,
                    TableNumber = tableNumber,
                    Server = server.Email,
                    FoodName = food.FoodName,
                    Price = food.Price,
                    Quantity = detail.Quantity,
                    OrderDate = detail.OrderDate,
                    SeatNumber = detail.SeatNumber
                });
            }

            return View(displayDetails);
        }


        // Edit a specific order summary with Id = id
        public async Task<IActionResult> EditOrder(int id)
        {
            var order = await _order.GetOrderById(id);
            var table = await _table.GetTableById(order.DiningTableId);
            var server = await _userMng.FindByIdAsync(order.ServerId);

            OrderDisplayModel displayOrder = new OrderDisplayModel
            {
                Id = id,
                TableNumber = table.TableNumber,
                Server = server.Email,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Total = order.Total,
                CreatedDate = order.CreatedDate,
                BillPaid = order.BillPaid
            };

            return View(displayOrder);
        }


        // Update the specific order summary in database
        //public async Task<IActionResult> UpdateOrder(OrderDisplayModel display)
        //{
        //    var table = await _table.GetTableByTableNumber(display.TableNumber);
        //    string[] fullName = display.Server.Split(" ");
        //    var server = await _people.GetPersonByFullName(fullName[0], fullName[1]);

        //    var order = new OrderModel
        //    {
        //        Id = display.Id,
        //        DiningTableId = table.Id,
        //        ServerId = server.Id,
        //        SubTotal = display.SubTotal,
        //        Tax = display.Tax,
        //        Total = display.Total,
        //        CreatedDate = display.CreatedDate,
        //        BillPaid = display.BillPaid
        //    };

        //    await _order.UpdateOrder(order);

        //    return RedirectToAction("ViewOrders");
        //}


        public byte[] GenerateInvoice(PrintModel order)
        {
            // Must have write permissions to the path folder
            var stream = new MemoryStream();
            using var writer = new PdfWriter(stream);
            using var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            Paragraph header = new Paragraph("Invoice")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));

            document.Add(newline);
            document.Add(header);

            // Add sub-header
            Paragraph subheader = new Paragraph("Thank you for your business from us.")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Add paragraph1
            Paragraph paragraph1 = new Paragraph($"This order was served by: {order.ServerName} on table {order.Table.TableNumber}");
            Paragraph paragraph3 = new Paragraph($"Time of order: {order.Time}");

            document.Add(paragraph1);

            document.Add(paragraph3);
            document.Add(newline);


            // Table
            Table table = new Table(2, true);
            Paragraph final = new Paragraph($"Final bill");
            document.Add(final);

            foreach (var detail in order.Quantities)
            {
                Cell cell11 = new Cell(1, 1)
                   .SetBackgroundColor(ColorConstants.WHITE)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(detail.Key));

                Cell cell12 = new Cell(1, 2)
    .SetBackgroundColor(ColorConstants.WHITE)
    .SetTextAlignment(TextAlignment.CENTER)
    .Add(new Paragraph(detail.Value.ToString()));

                table.AddCell(cell11);
                table.AddCell(cell12);
            }

            Cell totalText = new Cell(1, 1)
   .SetBackgroundColor(ColorConstants.WHITE)
   .SetTextAlignment(TextAlignment.CENTER)
   .Add(new Paragraph("Grand total"));
            table.AddCell(totalText
                );
            Cell total = new Cell(1, 2)
    .SetBackgroundColor(ColorConstants.WHITE)
    .SetTextAlignment(TextAlignment.CENTER)
    .Add(new Paragraph(order.TotalSum.ToString("$0.00")));
            table.AddCell(total
                );

            document.Add(newline);
            document.Add(table);
            document.Add(ls);

            document.Close();

            return stream.ToArray();

        }


        // Get the list of ordered food details for order summary with summary Id = id
        public async Task<IActionResult> PrintOrder(int id)
        {
            var order = await _order.GetOrderById(id);
            var orderDetails = await _order.GetOrderDetailsByOrderId(id);

            var detail = orderDetails.First();
            var table = await _table.GetTableById(detail.DiningTableId);
            var person = await _userMng.FindByIdAsync(detail.ServerId);

            var foods = new PrintModel();

            foreach (var orderDetail in orderDetails)
            {
                var food = await _food.GetFoodById(orderDetail.FoodId);
                var quantity = orderDetail.Quantity;

                if (!foods.Quantities.ContainsKey(food.FoodName))
                    foods.Quantities.Add(food.FoodName, quantity);
                else
                    foods.Quantities[food.FoodName] += quantity;
            }

            foods.ServerName = person.Email;
            foods.TotalSum = order.Total;
            foods.Table = table;
            foods.Time = order.CreatedDate;

            Response.ContentType = "application/pdf";
            Response.Headers.Add("Content-disposition", "attachment; filename=file.pdf"); // open in a new window

            var item = GenerateInvoice(foods);
            return File(item, "application/pdf");
        }


        // Get the list of ordered food details for order summary with summary Id = id
        public async Task<IActionResult> TableOrdersDetails(int tableId)
        {
            var table = await _table.GetTableById(tableId);
            var orderDetails = await _order.GetOrderDetailByDiningTable(table.Id);
            var displayDetails = new List<OrderDetailDisplayModel>();

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("NoOrderError", "Home");
            }
            else
            {
                var server = await _userMng.FindByIdAsync(orderDetails[0].ServerId);

                foreach (var detail in orderDetails)
                {
                    var food = await _food.GetFoodById(detail.FoodId);

                    displayDetails.Add(new OrderDetailDisplayModel
                    {
                        Id = detail.Id,
                        TableNumber = table.TableNumber,
                        Server = server.Email,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate
                    });
                }
                return View(displayDetails);
            }
        }


        // Delete a specific ordered food detail with Id = id
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var detail = await _order.GetOrderDetailById(id);
            var table = await _table.GetTableById(detail.DiningTableId);

            await _order.DeleteOrderDetail(id);

            return RedirectToAction("OrderDetailsByTableNumber", new { table.TableNumber });
        }


        // Delete a specific order summary with Id = id
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _order.DeleteOrder(id);

            return RedirectToAction("ViewOrders");
        }
    }
}