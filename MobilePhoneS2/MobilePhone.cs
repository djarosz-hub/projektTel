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
        private List<Contact> myContacts { get; set; }
        public Stack<string> History { get; set; }

        public MobilePhone(string myNumber)
        {
            MyNumber = myNumber;
            this.myContacts = new List<Contact>();
        }
        bool ContactExist(string newName2)
        {
            int checkname = -1;
            foreach (var x in myContacts)
            {
                if (x.Name == newName2)
                    checkname = 1;
            }
            if (checkname == 1)
            {
                Console.WriteLine("Contact with this name already exist on contact list. Not changed.");
                return false;
            }
            return true;
        }
        bool EmptyCheck(string cN)
        {
            return string.IsNullOrEmpty(cN);
        }
        bool PhoneNumberDigits(string phoneNumber)
        {
            foreach (char ch in phoneNumber)
            {
                if (!char.IsDigit(ch))
                    return false;
            }
            return true;
        }
        public void AddNewContact(string name, string phoneNumber)
        {
            Contact c = new Contact(name, phoneNumber);
            AddNewContact(c);
        }
        public void AddNewContact(Contact c)
        {
            bool flag = true;
            while (flag)
            {
                if (EmptyCheck(c.Name))
                {
                    Console.WriteLine("Contact name can not be empty, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;

                }
                if (EmptyCheck(c.PhoneNumber))
                {
                    Console.WriteLine("Contact number can not be empty, not added.");
                    Console.Write("Type any key to back to menu: ");
                    Console.ReadKey();
                    break;
                }
                if (!PhoneNumberDigits(c.PhoneNumber))
                {
                    Console.WriteLine("Number is not made only of digits, not added.");
                    BTM();
                    break;
                }
                if (FindContactIndex(c.Name) != -1)
                {
                    Console.WriteLine("Contact already exist on contact list, not added.");
                    BTM();
                    break;
                }
                myContacts.Add(c);
                Console.WriteLine("Contact sucesfully added to contact list");
                BTM();
                break;
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
                BTM();
            }
            else
            {
                Console.WriteLine($"Contact named {deleteName} doesn't exist");
                BTM();
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
                bool flag = true;
                if (!PhoneNumberDigits(newNumber))
                {
                    flag = false;
                    Console.WriteLine("New number is not made only of digits. Not changed.");
                    BTM();
                }
                if (EmptyCheck(newNumber))
                {
                    flag = false;
                    Console.WriteLine("New number can't be empty. Not changed.");
                    BTM();
                }
                if (flag)
                {
                    myContacts[tempResult].PhoneNumber = newNumber;
                    Console.WriteLine($"Succesfully changed.");
                    BTM();
                }
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
                if(i == 0)
                    Console.WriteLine();
                if (myContacts[i].PhoneNumber == tempNumber)
                {
                    counter++;
                    Console.WriteLine($"{myContacts[i].Name} -> {myContacts[i].PhoneNumber}");


                }
            }
            if (counter > 1)
            {
                Console.Write("Type name of contact You want to change: ");
                string oldName = Console.ReadLine();
                int tempIndex = FindContactIndex(oldName);
                if (tempIndex != -1)
                {
                    string oldNumber = myContacts[tempIndex].PhoneNumber;
                    Console.Write("Type new name for this contact: ");
                    string newName = Console.ReadLine();
                    if (ContactExist(newName))
                    {
                        myContacts[tempIndex].Name = newName;
                        Console.WriteLine("Succesfully changed");
                        BTM();
                    }
                    else
                        BTM();

                }
                else
                {
                    Console.WriteLine($"No contact named: {oldName} assigned to number: {tempNumber}.");
                    BTM();
                }

            }
            else if (counter == 1)
            {
                int tempIndex = FindNumberIndex(tempNumber);
                Console.Write($"Type new name for contact  {myContacts[tempIndex].Name} -> {myContacts[tempIndex].PhoneNumber}: ");
                string newName2 = Console.ReadLine();
                if (ContactExist(newName2))
                {
                    myContacts[tempIndex].Name = newName2;
                    Console.WriteLine("Succesfully changed.");
                    BTM();
                }
                else
                    BTM();
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
                BTM();

            }
        }
        public void CallContact()
        {
            Console.Write("Type name or number You want to call:");
            string callLenght = "";
            string calledContact = "";
            int flag = 0;
            string typedContact = Console.ReadLine();
            string trimedContact = typedContact.Trim(' ');
            if ((FindContactIndex(trimedContact) != -1) || (FindNumberIndex(trimedContact) != -1))
            {
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
                                Console.WriteLine($"{myContacts[i].Name} -> {myContacts[i].PhoneNumber}");
                            }
                        }
                        Console.Write("Type name You want to call: ");
                        string tempName = Console.ReadLine();
                        string trimmedTempName = tempName.Trim(' ');
                        if ((FindContactIndex(trimmedTempName) != -1) && (myContacts[FindContactIndex(trimmedTempName)].PhoneNumber == trimedContact))
                        {
                            callLenght += Timer(trimmedTempName);
                            calledContact += trimmedTempName;
                        }
                        else
                        {
                            Console.WriteLine($"Your contact list doesn't contains contact named {trimmedTempName} assigned to number: {trimedContact}");
                            flag = 1;
                            BTM();
                        }

                    }
                    else
                    {
                        string NumberTempName = myContacts[FindNumberIndex(trimedContact)].Name;
                        callLenght = Timer(NumberTempName);
                        calledContact = NumberTempName;
                    }
                }
                else
                {
                    callLenght = Timer(trimedContact);
                    calledContact = trimedContact;
                }

                if (flag == 0)
                {
                    StackPush(calledContact, callLenght);
                }
            }
            else
            {
                Console.Write("Contact not found on Your contact list, type any key to back to main menu:");
                Console.ReadKey();
                Console.WriteLine();
            }

        }
        public void StackPush(string calledContact, string callLenght)
        {
            DateTime moment = DateTime.Now;
            string callToPush = String.Format("{0}, connected on {1:00}/{2:00}/{3} at {4:00}:{5:00} for {6}.", calledContact, moment.Day, moment.Month, moment.Year, moment.Hour, moment.Minute, callLenght);
            History.Push(callToPush);
            BTM();
        }
        public string Timer(string cC)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //shows contact lenght and clean console view each second
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
            Console.ReadKey();
            return elapsedTime;
        }
        public void ShowContact()
        {
            Console.Write("Type phone number or name to find contact:");

            string findContact = Console.ReadLine();
            Console.Write("Contacts found: ");
            int result = -1;
            for (int i = 0; i < myContacts.Count; i++)
            {
                if (myContacts[i].Name == findContact || myContacts[i].PhoneNumber == findContact)
                {
                    Console.Write($"\n{myContacts[i].Name} -> {myContacts[i].PhoneNumber}");
                    result = 0;
                }
            }
            if (result != -1)
            {
                Console.Write("\nType any key to back to menu: ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("none.");
                BTM();
            }

        }
        public void PrintContacts()
        {
            if (myContacts.Count == 0)
            {
                Console.WriteLine("You contact list is empty.");
                BTM();
            }
            //sorts contacts before printing out
            else
            {
                myContacts.Sort(delegate (Contact x, Contact y) { return x.Name.CompareTo(y.Name); });
                for (int i = 0; i < myContacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {myContacts[i]} ");
                }
                BTM();
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
        public void LastCall()
        {
            void ByNameCall(string bnc)
            {
                string cL = Timer(bnc);
                StackPush(bnc, cL);
            }
            if (History.Count == 0)
            {
                Console.WriteLine("You didn't call anyone yet.");
                BTM();
            }
            else
            {
                string Lastcalled = History.Peek();
                Console.WriteLine($"Last called contact: {Lastcalled}");
                string cutLC = Lastcalled.Substring(0, Lastcalled.IndexOf(','));
                Console.WriteLine($"Do you want to call {cutLC} again? Type 1 to call again or any other key to back to menu");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice == 1)
                {
                    ByNameCall(cutLC);
                }
            }
        }
        public void BTM()
        {
            Console.Write("Type any key to back to menu: ");
            Console.ReadKey();
        }
    }
}
