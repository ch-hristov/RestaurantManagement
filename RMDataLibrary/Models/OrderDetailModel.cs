using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace RMDataLibrary.Models
{
    public class OrderDetailModel
    {
        // Id in the database
        public int Id { get; set; }

        // Id of the dining table that ordered this food 

        public int DiningTableId { get; set; }

        // Id of the server that serves this dining table
        public string ServerId { get; set; }   
        
        // Id of this ordered food
        public int FoodId { get; set; }

        // Order quantity for this food
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Id of order summary that contains this ordered food
        public int OrderId { get; set; }
        
        public string SeatNumber { get; set; }
    }
}
