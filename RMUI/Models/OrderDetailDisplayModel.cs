using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class OrderDetailDisplayModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Table Number.")]
        [Display(Name = "Table Number")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Server Name.")]
        public string Server { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Food Name.")]
        [Display(Name = "Food Name")]        
        public string FoodName { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency)]
        public string SeatNumber { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Quantity.")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public int OrderId { get; set; }
    }
}
