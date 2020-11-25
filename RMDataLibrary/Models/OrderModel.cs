using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class OrderModel
    {
        // Id in the database
        public int Id { get; set; }

        // Id of the dining table that owns this order summary
        public int DiningTableId { get; set; }

        // Id of the server that served this dining table
        public int ServerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Whether the bill for this order is paid
        public bool BillPaid { get; set; }
    }
}
