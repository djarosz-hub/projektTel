using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePhoneS2
{
    public class MobilePhone
    {
        public string MyNumber { get; set; } // read-write, only digits
        private List<Contact> myContacts;

        public MobilePhone(string myNumber)
        {
            MyNumber = myNumber;
            this.myContacts = new List<Contact>(); // empty list
        }

        public void AddNewContact(string name, string phoneNumber)
        {
            Contact c = new Contact(name, phoneNumber);
            myContacts.Add(c);
        }

        public void AddNewContact(Contact c)
        {
            myContacts.Add(c);
        }

        public void PrintContacts()
        {
            //foreach (var contact in myContacts)
            //{
            //    Console.WriteLine(contact);
            //}

            for (int i = 0; i < myContacts.Count; i++)
            {
                Console.WriteLine($"{i} {myContacts[i]}");
            }

        }
    }
}
