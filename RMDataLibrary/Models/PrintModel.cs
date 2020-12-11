using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class PrintModel
    {
        public string ServerName { get; set; }

        public int ServerId { get; set; }

        public Dictionary<string, int> Quantities { get; set; } = new Dictionary<string, int>();

        public decimal TotalSum { get; set; }

        public int Tax { get; set; }

        public DateTime Time { get; set; }
        public DiningTableModel Table { get; set; }
    }
}
