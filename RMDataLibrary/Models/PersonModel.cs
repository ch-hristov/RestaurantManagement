using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class PersonModel
    {
        // Id in database
        public int Id { get; set; }

        // Employee ID
        public int EmployeeID { get; set; }

        // Employee First Name
        public string FirstName { get; set; }
        
        // Employee Last Name
        public string LastName { get; set; }
        
        // Employee Email Address
        public string EmailAddress { get; set; }

        // Employee Cell Phone Number
        public string CellPhoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
