using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class DiningTableModel
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public int Seats { get; set; }

        public bool IsBlocked { get; set; }

        /// <summary>
        /// Whenever the table is deleted on the frontend
        /// it is actually only hidden to keep the records straight.
        /// </summary>
        public bool IsHidden { get; set; }

    }
}
