using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MobilePhoneS2
{
    public class MobilePhone
    {
        public string MyNumber { get; set; }
        private List<Contact> myContacts;
        public Stack<string> History { get; set; }

        public MobilePhone(string myNumber)
        {
            MyNumber = myNumber;
            this.myContacts = new List<Contact>();
        }
        public void AddNewContact(string name, string phoneNumber)
        {
            Contact c = new Contact(name, phoneNumber);
            AddNewContact(c);
        }
        public void AddNewContact(Contact c)
        {
            bool phoneNumberDigits(string phoneNumber)
            {
                foreach (char ch in phoneNumber)
                {
                    if (!char.IsDigit(ch))
                        return false;
                }
                return true;
            }
            bool EmptyNameCheck(string cNa)
            {
                return string.IsNullOrEmpty(cNa);
            }
            bool EmptyNumberCheck(string cNu)
            {
                return string.IsNullOrEmpty(cNu);
            }
            bool flag = true;
            while (flag)
            {
                if (EmptyNameCheck(c.Name))
                {
                    Console.WriteLine("Contact name can not be empty, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;

                }
                if (EmptyNumberCheck(c.PhoneNumber))
                {
                    Console.WriteLine("Contact number can not be empty, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;
                }
                if (!phoneNumberDigits(c.PhoneNumber))
                {
                    Console.WriteLine("Number is not made only of digits, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;
                }
                if (FindContactIndex(c.Name) != -1)
                {
                    Console.WriteLine("Contact already exist on contact list, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;
                }


                myContacts.Add(c);
                Console.WriteLine("Contact sucesfully added to contact list");
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
                break;
                //dublowanie przy modify

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
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Contact named {deleteName} doesn't exist");
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }

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
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Contact with this name not found");
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }

        }
        public void ModifyContactByNumber()
        {
            Console.WriteLine("Type contact number: ");
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
                //try { 
                string oldName = Console.ReadLine();
                //catch (Exception){

                //}

                int tempIndex = FindContactIndex(oldName);
                string oldNumber = myContacts[tempIndex].PhoneNumber;
                myContacts.RemoveAt(tempIndex);
                Console.Write("Type new name for this contact: ");
                string newName = Console.ReadLine();
                Contact y = new Contact(newName, oldNumber);
                AddNewContact(y);
                Console.WriteLine("Name sucessfully changed");
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();

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
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("none.");
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
            }

        }
        public void ShowHistory()
        {
            if (History.Count == 0)
            {
                Console.Write("Call history is empty, type any key to back to main menu:");
                Console.ReadKey();
                Console.WriteLine();
            }
            else
            {
                foreach (var x in History)
                {
                    Console.WriteLine(x);
                }
                Console.Write("Type any key to back to menu: ");
                Console.ReadKey();
                Console.WriteLine();
            }
        }
        public void CallContact()
        {
            Console.Write("Type name or number You want to call:");
            string callLenght = "";
            string calledContact = "";
            string callToPush;
            string NumberTempName = "";
            string typedContact = Console.ReadLine();
            string trimedContact = typedContact.Trim(' ');
            if ((FindContactIndex(trimedContact) != -1) || (FindNumberIndex(trimedContact) != -1))
            {
                Console.WriteLine("jest");
                bool CheckIfNumeric(string val)
                {
                    foreach (char ch in val)
                    {
                        if (!char.IsDigit(ch))
                            return false;
                    }
                    return true;
                }
                if (CheckIfNumeric(trimedContact))
                {
                    int counter = 0;
                    for (int i = 0; i < myContacts.Count; i++)
                    {
                        if (myContacts[i].PhoneNumber == trimedContact)
                        {
                            counter++;
                        }
                    }
                    if (counter > 1)
                    {
                        Console.WriteLine("Contacts found: ");
                        for (int i = 0; i < myContacts.Count; i++)
                        {
                            if (myContacts[i].PhoneNumber == trimedContact)
                            {
                                Console.WriteLine($"\n{myContacts[i].Name} -> {myContacts[i].PhoneNumber}");
                            }
                        }
                        Console.Write("Type name You want to call: ");
                        string tempName = Console.ReadLine();
                        string trimmedTempName = tempName.Trim(' ');
                        if ((FindContactIndex(trimmedTempName) != -1) && (myContacts[FindContactIndex(trimmedTempName)].Name == trimmedTempName))
                        {
                            callLenght = Timer(trimmedTempName);
                            calledContact = trimmedTempName;
                        }
                        else
                        {
                            Console.WriteLine($"Your contact list doesn't contains contact named {trimmedTempName} assigned to number: {trimedContact}");
                        }

                    }
                    else
                        NumberTempName = myContacts[FindNumberIndex(trimedContact)].Name;
                        callLenght = Timer(NumberTempName);
                        calledContact = trimedContact;
                }
                else
                {
                    callLenght = Timer(trimedContact);
                    calledContact = trimedContact;
                }
                string Timer(string cC)
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    while (!Console.KeyAvailable)
                    {
                        Console.Clear();
                        Console.WriteLine($"Calling: {cC}");
                        Console.WriteLine("Type any key to finish call.");
                        TimeSpan x = stopWatch.Elapsed;
                        string innerElapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                        x.Hours, x.Minutes, x.Seconds);
                        Console.WriteLine(innerElapsedTime);
                        Thread.Sleep(1000);
                    }

                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                        ts.Hours, ts.Minutes, ts.Seconds);
                    Console.Clear();
                    Console.WriteLine($"Finished call with {cC}. Call lenght: {elapsedTime}.");
                    return elapsedTime;
                }
                callToPush = $"{calledContact}, connected for: {callLenght}";
                History.Push(callToPush);
                
                //history.NewCall(callToPush);
                Console.WriteLine("Type any key to back to main menu.");
                Console.ReadKey();
                Console.WriteLine();
            }
            else Console.Write("Contact not found on Your contact list, type any key to back to main menu:");
            Console.ReadKey();
            Console.WriteLine();

        }
        //public void NewCall(string nC)
        //{
        //    history.Push(nC);
        //}

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

            myContacts.Sort(delegate (Contact x, Contact y) { return x.Name.CompareTo(y.Name); });
            for (int i = 0; i < myContacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {myContacts[i]} ");
            }
            Console.Write("Type any key to back to menu: ");
            Console.ReadKey();
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
