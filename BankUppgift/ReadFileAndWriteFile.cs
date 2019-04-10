using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankUppgift
{
    static class ReadFileAndWriteFile
    {
        public static Bank CreateBankAndReadFile(string bankFile)
        {
            Bank bank = new Bank();

            using (var reader = new StreamReader(bankFile))
            {
                int countOfCustomers = int.Parse(reader.ReadLine());

                for (int i = 0; i < countOfCustomers; i++)
                {
                    string line = reader.ReadLine();
                    string[] columns = line.Split(';');

                    var customer = new Customer
                    {
                        Id = int.Parse(columns[0]),
                        Corporate = columns[1],
                        Name = columns[2],
                        Adress = columns[3],
                        City = columns[4],
                        Region = columns[5],
                        Zip = columns[6],
                        Country = columns[7],
                        Phone = columns[8]
                    };

                    bank.Customers.Add(customer);
                }

                int countOfAccounts = int.Parse(reader.ReadLine());

                for (int i = 0; i < countOfAccounts; i++)
                {
                    string line = reader.ReadLine();
                    string[] columns = line.Split(';');

                    var account = new Account
                    {
                        Id = int.Parse(columns[0]),
                        CustomerId = int.Parse(columns[1]),
                        Balance = decimal.Parse(columns[2], CultureInfo.InvariantCulture)
                    };


                    bank.Accounts.Add(account);
                }   
            }
            return bank;
        }

        public static void WriteAndSave(Bank bank)
        {
            string dateTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            using (StreamWriter writer = new StreamWriter(@"C:\Dev\BankUppgift\" + dateTime + ".txt"))
            {
                writer.WriteLine(bank.Customers.Count);

                foreach (var customer in bank.Customers)
                {
                    writer.WriteLine($"{customer.Id};{customer.Corporate};{customer.Name};" +
                        $"{customer.Adress};{customer.City};{customer.Region};{customer.Zip};" +
                        $"{customer.Country};{customer.Phone}");
                }

                writer.WriteLine(bank.Accounts.Count);

                foreach (var account in bank.Accounts)
                {
                    writer.WriteLine($"{account.Id};{account.CustomerId};" +
                        $"{account.Balance.ToString(CultureInfo.InvariantCulture)}");
                }
            }
        }
    }
}
