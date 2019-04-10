using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankUppgift
{
    class Menu
    {
        private readonly Bank bank;

        public Menu(Bank bank)
        {
            this.bank = bank;
        }

        public void BankMenu()
        {
            PrintMenu();
            bool option = true;
            while (option)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                Console.Write(">  ");
                string menuSelect = Console.ReadLine().ToLower();
                switch (menuSelect)
                {
                    case "1":
                    case "search":
                        {
                            Console.Clear();
                            Console.WriteLine("Search for a customer.");
                            bank.SearchCustomer();
                            break;
                        }
                    case "2":
                    case "show":
                        {
                            Console.Clear();
                            Console.WriteLine("Show customer profile.");
                            bank.SerachByCustomerOrAccount();
                            break;
                        }
                    case "3":
                    case "create customer":
                        {
                            Console.Clear();
                            Console.WriteLine("Create a customer.");
                            bank.CreateCustomer();
                            break;
                        }
                    case "4":
                    case "delete customer":
                        {
                            Console.Clear();
                            Console.WriteLine("Delete a Customer");
                            bank.DeleteCustomer();
                            break;
                        }
                    case "5":
                    case "account":
                        {
                            Console.Clear();
                            Console.WriteLine("Create account.");
                            bank.CreateAccount();
                            break;
                        }
                    case "6":
                    case "delete account":
                        {
                            Console.Clear();
                            Console.WriteLine("Delete account.");
                            bank.DeleteAccount();
                            break;
                        }
                    case "7":
                    case "deposit":
                        {
                            Console.Clear();
                            Console.WriteLine("Make a deposit.");
                            bank.Deposit();
                            break;
                        }
                    case "8":
                    case "withdrawal":
                        {
                            Console.Clear();
                            Console.WriteLine("Make a withdrawal.");
                            bank.WithDrawal();
                            break;
                        }
                    case "9":
                    case "transfer":
                        {
                            Console.Clear();
                            Console.WriteLine("Transfer.");
                            bank.Transfer();
                            break;
                        }
                    case "10":
                    case "quit":
                        {
                            Console.Clear();
                            Console.WriteLine("Quit and Save.");
                            ReadFileAndWriteFile.WriteAndSave(bank);
                            bank.Statistics();
                            Console.WriteLine("Press any key to quit..");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;
                        }
                    case "11":
                    case "menu":
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Welcome to Bank 1.0!");
                            Console.WriteLine("Choose in the menu below.");
                            Console.WriteLine();
                            bank.Statistics();
                            Console.WriteLine();
                            PrintMenu();
                            break;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No option, choose between 1 - 10." +
                            "\nPress 11 - Menu. Press 10 - Quit & Save.");
                        break;
                }
            }
        }
        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Search for a customer." +
                "\n2. Show customer profile." +
                "\n3. Create a customer." +
                "\n4. Delete a Customer." +
                "\n5. Create account." +
                "\n6. Delete account." +
                "\n7. Make a deposit." +
                "\n8. Make a withdrawal." +
                "\n9. Transfer." +
                "\n10. Quit and Save.");
        }
    }
    
}
