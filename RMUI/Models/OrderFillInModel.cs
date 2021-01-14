using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class OrderFillInModel
    {
        [Required]
        [Display(Name = "Table Number")]
        public int TableNumber { get; set; }

        [Required]
        [Display(Name = "Server")]
        public string Server { get; set; }

        [Display(Name = "Food Type")]
        [Required]
        public string FoodType { get; set; }

        [Display(Name = "Food Name")]
        [Required]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Seat number")]
        [Range(1, 4)]
        public int SeatNumber { get;  set; }

    }
}
