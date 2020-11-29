using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class OrderStatistic
    {
        public FoodModel FoodName { get; set; }
        public int Amount { get; set; } = 0;
    }
}
