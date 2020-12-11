using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IOrderData
    {
        Task<List<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderByTable(int tableId);
        Task<List<OrderDetailModel>> GetOrderDetailByDiningTable(int tableId);
        Task<OrderModel> InsertOrderByTable(int tableId);
        Task InsertOrderDetail(OrderDetailModel orderDetail);
        Task<OrderDetailModel> GetOrderDetailById(int id);

        Task<List<OrderDetailModel>> GetOrderDetailsByOrderId(int orderId);
        Task<OrderModel> GetOrderById(int id);

        Task PayBill(OrderModel order);
        Task UpdateOrderDetail(OrderDetailModel detail);
        Task UpdateOrder(OrderModel order);
        Task DeleteOrderDetail(int id);
        Task DeleteOrder(int id);
        Task<List<OrderModel>> GetAllOrderRecords();
        Task<List<OrderDetailModel>> GetAllOrderDetailRecords();


    }
}