using System;
using System.Collections.Generic;

namespace SuperAdmin.Models
{
    public class Capability
    {
        public string ID { get; set; }

        public dynamic Value { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
