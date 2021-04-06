using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class PermissionSource
    {
        public int Id { get; set; }

        [DisplayName("Can work with tables")]
        public bool CanWorkWithTables { get; set; } = true;

        [DisplayName("Can work with orders")]
        public bool CanWorkWithOrders { get; set; } = true;

        [DisplayName("Can work with ads")]
        public bool CanWorkWithAds { get; set; } = true;

        [DisplayName("Can work with foods")]
        public bool CanWorkWithFoods { get; set; } = true;

        [DisplayName("Can people register?")]
        public bool CanPeopleRegister { get; set; } = true;
    }
}
