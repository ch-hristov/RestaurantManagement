using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class FoodModel
    {
        // Id in database
        public int Id { get; set; }
        
        // Type of food
        public string FoodType { get; set; }

        // Name of food
        public string FoodName { get; set; }

        // Price of food
        public decimal Price { get; set; }

        // Id of Food Type for this food
        public int TypeId { get; set; }
    }
}
