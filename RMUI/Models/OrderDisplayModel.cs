using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class OrderDisplayModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Table Number.")]
        [Display(Name = "Table Number")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Server Name.")]
        public string Server { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal SubTotal { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Tax { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Bill Paid")]
        public bool BillPaid { get; set; }
    }
}
