using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Model
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public category CustomerGroup { get; set; }

        public Customer (string firstName, string lastName, category group)
        {
            FirstName = firstName;
            LastName = lastName;
            CustomerGroup = group;
        }

        public override string ToString()
        {
            return String.Format("name: {0} {1} category: {2}", FirstName.PadLeft(15), LastName.PadLeft(15), CustomerGroup);
        }

        public bool Equals(Customer obj)
        {
            if (obj == null) return false;

            if (FirstName == obj.FirstName && LastName == obj.LastName && CustomerGroup == CustomerGroup) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode()+LastName.GetHashCode();
        }
    }
}

public enum category { regular, businesclass, vip}
