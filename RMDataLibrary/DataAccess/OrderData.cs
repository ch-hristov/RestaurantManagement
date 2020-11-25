using Microsoft.Extensions.Configuration;
using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class OrderData : IOrderData
    {
        private readonly ISqlDataAccess _sql;
        private readonly IFoodData _food;
        private readonly IConfiguration _config;

        public OrderData(ISqlDataAccess sql, IFoodData food, IConfiguration config)
        {
            _sql = sql;
            _food = food;
            _config = config;
        }

        // Insert detail of one ordered food for a specific dining table to the database
        // detail includes ordered food detail Id, dining table Id, server Id, food Id, food quantity, food price and order date
        public async Task InsertOrderDetail(OrderDetailModel orderDetail)
        {
            await _sql.SaveData<OrderDetailModel>("OrderDetail_Insert", orderDetail);
        }


        // Insert summary of order by a specific dining table to the database
        // summary includes order summary Id, dining table Id, server Id, subTotal, tax, total, date and bill info
        // Each dining table has a specific order summary
        public async Task InsertOrderByTable(int tableId)
        {
            OrderModel order = new OrderModel();
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("OrderDetail_GetByDiningTable", new { DiningTableId = tableId });

            if (results == null) return;

            order.DiningTableId = tableId;
            order.ServerId = results[0].ServerId;

            foreach (var detail in results)
            {
                var food = await _food.GetFoodById(detail.FoodId);
                order.SubTotal += detail.Quantity * food.Price;
            }
                                 
            decimal taxRate = GetTaxRate();
            order.Tax = order.SubTotal * taxRate;
            order.Total = order.SubTotal + order.Tax;

            await _sql.SaveData<OrderModel>("Order_Insert", order);
        }


        // Get order summary for all the dining tables
        public async Task<List<OrderModel>> GetAllOrders()
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("Order_GetAll", new { });

            return results;
        }


        // Get summary of order for a specific dining table with Id = tableId
        public async Task<OrderModel> GetOrderByTable(int tableId)
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("Order_GetByDiningTable", new { DiningTableId = tableId });

            return results.FirstOrDefault();
        }


        // Get list of food ordered details by a specific dining table with Id = tableId 
        public async Task<List<OrderDetailModel>> GetOrderDetailByDiningTable(int tableId)
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("OrderDetail_GetByDiningTable", new { DiningTableId = tableId });

            return results;
        }
      

        // Retrieve Tax Rate info from appsettings.json 
        private decimal GetTaxRate()
        {
            string rateText = _config.GetValue<string>("TaxRate");

            bool isValidTaxRate = decimal.TryParse(rateText, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not properly set.");
            }

            output = output / 100;

            return output;
        }


        // Get a specific ordered food detail with Id = id
        public async Task<OrderDetailModel> GetOrderDetailById(int id)
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("OrderDetail_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Get a specific order summary with Id = id
        public async Task<OrderModel> GetOrderById(int id)
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("Order_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Update an ordered food detail
        public async Task UpdateOrderDetail(OrderDetailModel detail)
        {
            await _sql.SaveData<OrderDetailModel>("OrderDetail_Update", detail);
        }


        // Update an order summary
        public async Task UpdateOrder(OrderModel order)
        {
            await _sql.SaveData<OrderModel>("Order_Update", order);
        }


        // Delete a specific ordered food detail with Id = id
        public async Task DeleteOrderDetail(int id)
        {
            await _sql.DeleteData<dynamic>("OrderDetail_Delete", new { id });
        }


        // Delete a specific order summary with Id = id
        public async Task DeleteOrder(int id)
        {
            await _sql.DeleteData<dynamic>("Order_Delete", new { id });
        }


        public async Task<List<OrderModel>> GetAllOrderRecords()
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("Order_GetAllRecords", new { });

            return results;
        }


        public async Task<List<OrderDetailModel>> GetAllOrderDetailRecords()
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("OrderDetail_GetAllRecords", new { });

            return results;
        }
    }
}
