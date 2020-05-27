using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePhoneS2
{
    //immutable
    public class Contact
    {
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; } // only digits

        public Contact(string name, string phoneNumber)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
        }

        public override string ToString() => $"{Name} -> {PhoneNumber}";

    }
}
