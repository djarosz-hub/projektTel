using System;

namespace MobilePhoneS2
{
    class Program
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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MobilePhone myPhone = new MobilePhone("123456789");
            do
            {
                PrintAction();
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 0:
                        Console.WriteLine("Shutting down ...");
                        Environment.Exit(0);
                        break;
                    case 1:
                        myPhone.PrintContacts();
                        break;
                    case 2:
                        myPhone.AddNewContact(ReadNewContact());
                        break;
                }
            }
            while (true);
        }

        private static void PrintAction()
        {
            Console.WriteLine("Avaible actions:");
            Console.WriteLine("0 - shut down");
            Console.WriteLine("1 - print all contacts");
            Console.WriteLine("2 - add new contact");
            // ...
            Console.Write("Choose action: ");
        }

        private static Contact ReadNewContact()
        {
            Console.Write("Enter new contact name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new contact phone number: ");
            string phoneNumber = Console.ReadLine();
            return new Contact(name, phoneNumber);
        }

    }
}
