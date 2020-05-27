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
            this.myContacts = new List<Contact>();
        }
        public void AddNewContact(string name, string phoneNumber)
        {
            Contact c = new Contact(name, phoneNumber);
            //myContacts.Add(c);
            AddNewContact(c);
        }
        public void AddNewContact(Contact c)
        {
            if (FindContactIndex(c.Name) == -1) //not found
            {
                myContacts.Add(c);
            }
            else
            {
                Console.WriteLine("Contact exists, not added");
            }

        }
        public void RemoveContact(string deleteName)
        {
            int index = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].Name == deleteName)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                myContacts.RemoveAt(index);
                Console.WriteLine("Sucesfully removed");
            }
            else
            {
                Console.WriteLine($"Contact named {deleteName} doesn't exist");
            }


            //int index = FindContactIndex(c.Name);
            //if(index == -1)
            //{
            //    Console.WriteLine("Contact named doesn't exist in contact list");
            //}
            //else
            //{
            //    myContacts.RemoveAt(index);
            //}
        }
        public void ModifyContactByName()
        {
            Console.Write("Type name of contact: ");
            string tempName = Console.ReadLine();
            int tempResult = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].Name == tempName)
                {
                    tempResult = i;
                    break;
                }
            }
            if (tempResult != -1)
            {
                Console.WriteLine($"Contact found => name: {myContacts[tempResult].Name}, actual number: {myContacts[tempResult].PhoneNumber}");
                Console.Write("Type new number: ");
                string newNumber = Console.ReadLine();
                //myContacts[tempResult].PhoneNumber = newNumber;
                myContacts.RemoveAt(tempResult);
                Contact x = new Contact(tempName, newNumber);
                AddNewContact(x);
                Console.WriteLine($"Sucesfully changed number on {tempName}");
            }
            else Console.WriteLine("Contact with this name not found");

        }
        public void ModifyContactByNumber()
        {
            Console.WriteLine("Type number of contact: ");
            string tempNumber = Console.ReadLine();
            int counter = 0;

            Console.Write("Contacts found: ");
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].PhoneNumber == tempNumber)
                {
                    counter++;
                    Console.WriteLine($"\n{myContacts[i].Name} -> {myContacts[i].PhoneNumber}");


                }
            }
            if (counter > 1)
            {
                Console.Write("Type name of contact You want to change: ");
                string oldName = Console.ReadLine();

                int tempIndex = FindContactIndex(oldName);
                string oldNumber = myContacts[tempIndex].PhoneNumber;
                myContacts.RemoveAt(tempIndex);
                Console.Write("Type new name for this contact: ");
                string newName = Console.ReadLine();
                Contact y = new Contact(newName, oldNumber);
                AddNewContact(y);
                Console.WriteLine("Name sucessfully changed");

            }
            else if (counter == 1)
            {
                int tempIndex = FindNumberIndex(tempNumber);
                Console.Write($"Type new name for contact  {myContacts[tempIndex].Name} -> {myContacts[tempIndex].PhoneNumber}: ");
                string newName2 = Console.ReadLine();
                myContacts.RemoveAt(tempIndex);
                Contact k = new Contact(newName2, tempNumber);
                AddNewContact(k);
                Console.WriteLine("Name succesfully changed");
            }
            else
            {
                Console.WriteLine("none.");
            }
            ////Console.Write("Type number of contact: ");
            ////string tempNumber = Console.ReadLine();
            ////int tempResult = -1;
            ////for (int i = 0; i < myContacts.Count; i++)
            ////{
            ////    if (myContacts[i].PhoneNumber == tempNumber)
            ////    {
            ////        tempResult = i;
            ////        break;
            ////    }
            ////}
            ////if (tempResult != -1)
            ////{
            ////    Console.WriteLine($"Contact found => name: {myContacts[tempResult].Name}, number: {myContacts[tempResult].PhoneNumber}");
            ////    Console.Write("Type new name: ");
            ////    string newName = Console.ReadLine();
            ////    //myContacts[tempResult].PhoneNumber = newNumber;
            ////    myContacts.RemoveAt(tempResult);
            ////    Contact z = new Contact(newName, tempNumber);
            ////    AddNewContact(z);
            ////    Console.WriteLine($"Sucesfully changed name to: {newName}");
            ////}
            ////else Console.WriteLine("Contact with this name not found");
        }
        public void ShowContact()
        {
            Console.Write("Type phone number or name to find contact:");
            string findContact = Console.ReadLine();
            int result = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].Name == findContact || myContacts[i].PhoneNumber == findContact)
                {
                    result = i;
                    break;
                }
            }
            if (result != -1)
            {
                Console.WriteLine($"Contact found: {myContacts[result].Name} -> {myContacts[result].PhoneNumber}");
            }
            else Console.WriteLine("Contact not found");

        }
        public void PrintContacts()
        {
            //foreach (var contact in myContacts)
            //{
            //    Console.WriteLine(contact);
            //}
            //****           myContacts.Sort();
            for (int i = 0; i < myContacts.Count; i++)
            {
                Console.WriteLine($"{i} {myContacts[i]} ");
            }
        }
        private int FindContactIndex(string name)
        {
            int result = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].Name == name)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        private int FindNumberIndex(string findNumber)
        {
            int result = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].PhoneNumber == findNumber)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}
