using System;

namespace MobilePhoneS2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to MyPhoneApp!\n");

            MobilePhone myPhone = new MobilePhone("123456789");
            do
            {
                PrintAction();
                int action;
                while (true)
                {
                    try
                    {
                        action = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Type correct value");
                    }

                }
                switch (action)
                {
                    case 0:
                        Console.WriteLine("Shutting down...");
                        Environment.Exit(0);
                        break;
                    case 1:
                        myPhone.PrintContacts();
                        break;
                    case 2:
                        myPhone.AddNewContact(ReadNewContact());
                        break;
                    case 3:
                        Console.WriteLine("Type contact name You want to delete:");
                        string deleteName = Console.ReadLine();
                        myPhone.RemoveContact(deleteName);
                        break;
                    case 4:
                        Console.WriteLine("Available actions:");
                        Console.WriteLine("1 - change number assigned to contact");
                        Console.WriteLine("2 - change name assigned to number");
                        int modifyAction;
                        while (true)
                        {
                            try
                            {
                                modifyAction = int.Parse(Console.ReadLine());
                                break;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Type correct value from list");
                            }
                        }
                        switch (modifyAction)
                        {
                            case 1:
                                myPhone.ModifyContactByName();
                                break;
                            case 2:
                                myPhone.ModifyContactByNumber();
                                break;
                            default:
                                Console.WriteLine("Wrong type, type any key to back to main menu.");
                                Console.ReadKey();
                                break;
                        }
                        break;
                    case 5:
                        myPhone.ShowContact();
                        break;
                    default:
                        Console.WriteLine("Type correct number from action list.");
                        Console.Write("Type any key to back to menu: ");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (true);


        }

        private static void PrintAction()
        {
            Console.WriteLine("You are in main menu, available actions:");
            Console.WriteLine("0 - shut down");
            Console.WriteLine("1 - print all contacts");
            Console.WriteLine("2 - add new contact");
            Console.WriteLine("3 - remove contact");
            Console.WriteLine("4 - modify contact");
            Console.WriteLine("5 - find contact");
            //6 - call contact
            //7 calls history
            Console.Write("Choose action: ");

        }
        private static Contact ReadNewContact()
        {
            Console.Write("Enter new contact name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new phone number: ");
            string phoneNumber = Console.ReadLine();

            return new Contact(name, phoneNumber);

        }
    }
}
