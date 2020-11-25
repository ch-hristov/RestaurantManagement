using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class FoodTypeDisplayModel
    {
        public int Id { get; set; }

        [Display(Name = "Food Type")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "You need to provide a long enough Food Type.")]
        public string FoodType { get; set; }

    }
}
