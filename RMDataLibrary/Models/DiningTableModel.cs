using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class DiningTableModel
    {
        // Id in the database
        public int Id { get; set; }

        // DiningTable number
        public int TableNumber { get; set; }

        // Number of seats in this Diningtable
        public int Seats { get; set; }

        // Whether the table is blocked or not
        public bool IsBlocked { get; set; }

    }
}
