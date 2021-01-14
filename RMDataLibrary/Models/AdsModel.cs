using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class AdsModel
    {
        public int Id { get; set; }

        public string Ad1 { get; set; } = "";

        public string Ad2 { get; set; } = "";

        public string Ad3 { get; set; } = "";

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Ad1Blocked { get; set; }
        public bool Ad2Blocked { get; set; }
        public bool Ad3Blocked { get; set; }
    }
}
