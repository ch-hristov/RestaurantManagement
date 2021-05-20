using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class PaymentOptionsModel
    {
        public int Id { get; set; }

        [DisplayName("Company payments")]
        public bool CompanyPayments { get; set; }

        public bool Cash { get; set; }

        [DisplayName("Visa cards")]
        public bool VisaCards { get; set; }

        [DisplayName("Mastercard")]
        public bool MasterCardCards { get; set; }

        [DisplayName("Maestro")]
        public bool MaestroCards { get; set; }

        [DisplayName("Diners")]
        public bool DinersCards { get; set; }
        [DisplayName("American Express")]

        public bool AmericanExpressCards { get; set; }

        [DisplayName("Apple pay")]
        public bool ApplePay { get; set; }
    }
}
