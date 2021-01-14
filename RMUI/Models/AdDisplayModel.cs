using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class AdDisplayModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Advertisement 1")]
        [Url]
        public string Ad1 { get; set; }

        [Display(Name = "Advertisement 2")]
        [Required]
        [Url]
        public string Ad2 { get; set; }

        [Display(Name = "Advertisement 3")]
        [Required]
        [Url]
        public string Ad3 { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "Blocked")]
        public bool Ad1Blocked { get;  set; }

        [Display(Name = "Blocked")]

        public bool Ad3Blocked { get;  set; }

        [Display(Name = "Blocked")]

        public bool Ad2Blocked { get;  set; }
    }
}
