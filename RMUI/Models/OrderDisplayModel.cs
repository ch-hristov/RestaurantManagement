using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
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


        public OrderDisplayModel() { }

        public async static Task<OrderDisplayModel> FromDetails(
            IEnumerable<OrderDetailModel> details,
            IFoodData _food,
            decimal taxRate,
            string server,
            int tableNumber)
        {
            decimal subTotal = 0;
            var firstOrder = details.OrderBy(x => x.OrderDate).First();
            foreach (var detail in details)
            {
                var food = await _food.GetFoodById(detail.FoodId);
                subTotal += detail.Quantity * food.Price;
            }

            var tax = subTotal * taxRate;
            var total = subTotal + tax;

            return new OrderDisplayModel
            {
                BillPaid = false,
                Server = server,
                CreatedDate = firstOrder.OrderDate,
                SubTotal = subTotal,
                Tax = tax,
                Total = total,
                TableNumber = tableNumber
            };
        }

    }
}
