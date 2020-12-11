using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class DiningTableDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Table Number")]
        [Range(1, 100, ErrorMessage = "You need to enter a valid Table Number.")]
        public int TableNumber { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "You need to enter a valid number for Seats.")]
        public int Seats { get; set; }

        [Required]
        [Display(Name = "Blocked")]
        public bool IsBlocked { get; set; }
    }
}
