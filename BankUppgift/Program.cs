using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankUppgift
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = ReadFileAndWriteFile.CreateBankAndReadFile(@"C:\Dev\BankUppgift\bankdata.txt");
            Menu menu = new Menu(bank);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Welcome to Bank 1.0!");
            Console.WriteLine("Choose in the menu below.");
            Console.WriteLine();
            bank.Statistics();
            menu.BankMenu();
        }
    }
}
