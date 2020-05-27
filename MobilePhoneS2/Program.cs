using System;

namespace MobilePhoneS2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aplikacja symulująca zachowanie się telefonu - listy kontaktów
            // Kontakt - nazwa i przypisany jej numer telefonu
            // Numer telefonu - ciąg cyfr
            // Lista kontaktów przechowywana w strukturze List<T>
            // Funkcjonalność:
            // * dodanie/usunięcie/modyfikacja/wypisanie kontaktu
            // * wypisanie całej listy kontaktów
            // * wyszukanie numeru telefonu dla konkretnego kontaktu
            // * wyszukanie nazw kontaktów dla podanego numeru telefonu
            // Założenie: lista kontaktów jest - poza wymienionymi operacjami - niewidoczna/niedostępna
            // Aplikacja na konsoli sterowana prostym menu
            Console.WriteLine("Hello World!");
            MobilePhone myPhone = new MobilePhone("123456789");
            do
            {
                PrintAction();
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 0:
                        Console.WriteLine("Shutting down...");
                        Environment.Exit(0);
                        break;
                    case 1:
                        //print all contacts
                        myPhone.PrintContacts();
                        break;
                    case 2:
                        //add new contact
                        myPhone.AddNewContact(ReadNewContact());
                        break;
                    case 3:
                        Console.WriteLine("Type contact name You want to delete:");
                        string deleteName = Console.ReadLine();
                        myPhone.RemoveContact(deleteName);
                        //remove contact
                        break;
                    case 4:
                        // modify contact by name or by number
                        Console.WriteLine("Available actions:");
                        Console.WriteLine("1 - change number assigned to contact");
                        Console.WriteLine("2 - change name assigned to number");
                        int modifyAction = int.Parse(Console.ReadLine());
                        switch (modifyAction)
                        {
                            case 1:
                                myPhone.ModifyContactByName();
                                break;
                            case 2:
                                myPhone.ModifyContactByNumber();
                                break;
                        }
                        break;
                    case 5:
                        myPhone.ShowContact();
                        // show contact, type name or number
                        break;






                }
            } while (true);


        }
        private static void PrintAction()
        {
            Console.WriteLine("Available actions:");
            Console.WriteLine("0 - shut down");
            Console.WriteLine("1 - print all contacts");
            Console.WriteLine("2 - add new contact");
            Console.WriteLine("3 - remove contact");
            Console.WriteLine("4 - modify contact");
            Console.WriteLine("5 - find contact");

            //..
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
