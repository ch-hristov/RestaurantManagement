using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class PersonDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "You need to provide a long enough First Name.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "You need to provide a long enough Last Name.")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Cell Phone Number")]
        [Phone]
        public string CellPhoneNumber { get; set; }


        [Display(Name = "Full Name")]
        public string FullName {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
