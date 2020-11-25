using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class BillData : IBillData
    {
        private readonly ISqlDataAccess _sql;

        public BillData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        // Pay Bill Button press will insert bill info into database
        // and update BillPaid info in order and orderDetail tables
        public async Task InsertBill(BillModel bill)
        {
            await _sql.SaveData("Order_UpdateBillPaid", new { Id = bill.OrderId });
            await _sql.SaveData("OrderDetail_UpdateBillPaid", new { bill.DiningTableId, bill.OrderId});
            await _sql.SaveData("Bill_Insert", bill);
        }
    }
}
