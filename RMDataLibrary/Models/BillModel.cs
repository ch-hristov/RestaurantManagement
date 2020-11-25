using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace RMDataLibrary.Models
{
    public class BillModel
    {
        // Id in the database
        public int Id { get; set; }

        // Id of the order summary for this bill
        public int OrderId { get; set; }

        // Id of the dining table for this bill
        public int DiningTableId { get; set; }

        // Id of the server that served this table
        public int ServerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;

        // Bill is paid or not
        public bool BillPaid { get; set; }
    }
}
