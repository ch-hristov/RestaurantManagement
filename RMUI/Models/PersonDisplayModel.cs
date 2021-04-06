using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class PersonDisplayModel
    {
        public string Id { get; set; }


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


        [Display(Name = "Do not allow to become admin?")]
        public bool DoNotAllowToBecomeAdmin { get; set; }


        [Display(Name = "Do not allow to become manager?")]
        public bool DoNotAllowToBecomeManager { get; set; }


        [Display(Name = "Do not allow to become waiter?")]
        public bool DoNotAllowToBecomeServer { get; set; }


        [Display(Name = "Is administrator?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Is manager?")]
        public bool IsManager { get; set; }

        [Display(Name = "Is server?")]
        public bool IsWaiter { get; set; }

        [Display(Name = "Full Name")]
        public string FullName {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
