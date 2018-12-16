using System;
using System.Collections.Generic;
using System.Text;

namespace CMOV_Stocks
{
    class Company {
        public string Code { get; set; }
        public string Name { get; set; }

        public Company(string _code, string _name) {
            Code = _code;
            Name = _name;
        }

        public bool Equals(Company other) {
            if (other == null)
                return false;
            if (Code == other.Code)
                return true;
            else
                return false;
        }

        
        public override string ToString() {
            return Code + " - " + Name;
        }
    }
}
