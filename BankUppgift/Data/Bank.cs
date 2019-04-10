using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUppgift
{
    class Bank
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Account> Accounts { get; set; } = new List<Account>();

        public void SearchCustomer()
        {
            Console.Write("Enter Name or City: ");
            string userInput = Console.ReadLine().ToLower();

            var searchCustomer = (
                from customer in Customers
                where customer.Name.ToLower().Contains(userInput) ||
                customer.City.ToLower().Contains(userInput)
                select customer).ToList();

            if (searchCustomer.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No match in the bank.");
            }

            Console.WriteLine();
            foreach (var item in searchCustomer)
            {
                Console.WriteLine("ID: " + item.Id + " Name: " + item.Name + "\tCity: " + item.City);
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void SerachByCustomerOrAccount()
        {
            Console.WriteLine("Press 1 to seacrh by Customer ID.");
            Console.WriteLine("Press 2 to search by Account ID.");

            bool option = true;
            while (option)
            {
                string userInput = Console.ReadLine();
                int userInputMenu;
                bool result = int.TryParse(userInput, out userInputMenu);
                if (userInputMenu == 1)
                {
                    ShowCustomerByCustomerId();
                    option = false;
                }
                else if (userInputMenu == 2)
                {
                    ShowCustomerByAccountID();
                    option = false;
                }
                else
                {
                    Console.WriteLine("Not a valid option.");
                }
            }
        }

        public void ShowCustomerByAccountID()
        {
            Account account = CheckCustomerAccountId();
            var customer = Customers.SingleOrDefault(c => c.Id == account.CustomerId);

            Console.WriteLine();
            Console.WriteLine("Customer number: " + customer.Id + "\nCoporate number: " + customer.Corporate +
                    "\nCompany Name: " + customer.Name + "\nAdress: " + customer.Adress +
                    "\nCity: " + customer.City + "\nRegion: " + customer.Region +
                    "\nZip Code: " + customer.Zip + "\nCountry: " + customer.Country +
                    "\nPhone number: " + customer.Phone);

            var allAccount = (from a in Accounts where a.CustomerId == customer.Id select a).ToList();

            var totsum = (from a in Accounts where a.CustomerId == customer.Id select a.Balance).Sum();

            Console.WriteLine();
            foreach (var item in allAccount)
            {
                Console.WriteLine("Accounts: " + item.Id + ", Balance: " + item.Balance + ":-");
            }

            Console.WriteLine("Total Balance: " + totsum + ":-");
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void ShowCustomerByCustomerId()
        {
            Customer customer = CheckCustomerId();
            Console.WriteLine();
            Console.WriteLine(
                "Customer number: " + customer.Id + "\nCoporate number: " + customer.Corporate +
                "\nCompany Name: " + customer.Name + "\nAdress: " + customer.Adress +
                "\nCity: " + customer.City + "\nRegion: " + customer.Region +
                "\nZip Code: " + customer.Zip + "\nCountry: " + customer.Country +
                "\nPhone number: " + customer.Phone);

            var allAccount = (from a in Accounts where a.CustomerId == customer.Id select a).ToList();

            var totsum = (from a in Accounts where a.CustomerId == customer.Id select a.Balance).Sum();

            Console.WriteLine();
            foreach (var item in allAccount)
            {
                Console.WriteLine("Accounts: " + item.Id + ", Balance: " + item.Balance + ":-");
            }

            Console.WriteLine("Total Balance: " + totsum + ":-");
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void CreateCustomer()
        {
            var newCustomer = new Customer();
            newCustomer.Id = GetAvailableCustomerId();
            Console.WriteLine();
            Console.WriteLine("Your new Customer ID number: " + newCustomer.Id);

            var newAccount = new Account();
            newAccount.Id = GetAvailableAccountId();
            Console.WriteLine("Your new account number: " + newAccount.Id);
            Console.WriteLine("Balance: " + newAccount.Balance + ":-");

            newAccount.CustomerId = newCustomer.Id;

            Console.Write("\nEnter Corporate-number: ");
            newCustomer.Corporate = Console.ReadLine();
            Console.Write("Enter Name: ");
            newCustomer.Name = Console.ReadLine();
            Console.Write("Enter Adress: ");
            newCustomer.Adress = Console.ReadLine();
            Console.Write("Enter City: ");
            newCustomer.City = Console.ReadLine();
            Console.Write("Enter Region: ");
            newCustomer.Region = Console.ReadLine();
            Console.Write("Enter Zip: ");
            newCustomer.Zip = Console.ReadLine();
            Console.Write("Enter Country: ");
            newCustomer.Country = Console.ReadLine();
            Console.Write("Enter Phone: ");
            newCustomer.Phone = Console.ReadLine();

            var waitForAnswer = true;
            while (waitForAnswer)
            {
                Console.WriteLine("\nDo you want to save this profile? Yes or No: ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "yes")
                {
                    Customers.Add(newCustomer);
                    Accounts.Add(newAccount);
                    Console.WriteLine();
                    Console.WriteLine("The customer has been saved.");
                    waitForAnswer = false;
                }
                else if (userInput == "no")
                {
                    Console.WriteLine("The customer is not saved. Press 11 for menu.");
                    waitForAnswer = false;
                }
                else
                {
                    Console.WriteLine("Please enter Yes or No to continue.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void DeleteCustomer()
        {
            Customer customer = CheckCustomerId();

            Console.WriteLine();
            Console.WriteLine("Name: " + customer.Name +
                              "\nAdress: " + customer.Adress +
                              "\nCity: " + customer.City +
                              "nRegion: " + customer.Region +
                              "\nZip Code: " + customer.Zip +
                              "\nCountry: " + customer.Country);

            Console.WriteLine();
            Console.WriteLine("Do you want to delete this customer? Yes or No.");
            string yesOrNo = Console.ReadLine().ToLower();

            var customerAccounts = Accounts.Where(account => account.CustomerId == customer.Id).ToList();

            if (yesOrNo == "yes")
            {
                if (customerAccounts.Count > 0)
                {
                    Console.WriteLine("Can't delete a customer with accounts connected. Delete accounts first.");
                }
                else
                {
                    Console.WriteLine("The customer has been deleted");
                    Customers.Remove(customer);
                }
            }
            else if (yesOrNo == "no")
            {
                Console.WriteLine("Didn't remove the account.");
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void CreateAccount()
        {
            Customer customer = CheckCustomerId();

            var newAccountNumber = new Account();
            newAccountNumber.Id = GetAvailableAccountId();
            Console.WriteLine("Your new account number is: " + newAccountNumber.Id);

            decimal startAmount = 0;
            newAccountNumber.Balance = startAmount;
            Console.WriteLine("Amount: " + startAmount + ":-");

            newAccountNumber.CustomerId = customer.Id;
            Accounts.Add(newAccountNumber);

            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void DeleteAccount()
        {
            Account account = CheckCustomerAccountId();

            Console.WriteLine("Do you want to delete this account? Yes or No");
            Console.WriteLine("ID: " + account.Id + "\nAmount: " + account.Balance + ":-");

            var waitForAnswer = true;
            while (waitForAnswer)
            {
                string yesOrNo = Console.ReadLine().ToLower();
                if (yesOrNo == "yes")
                {
                    if (account.Balance > 0)
                    {
                        Console.WriteLine("You can't delete an account with money on it.");
                        waitForAnswer = false;
                    }
                    else
                    {
                        Accounts.Remove(account);
                        Console.WriteLine("Account has been deleted.");
                        waitForAnswer = false;
                    }
                }
                else if (yesOrNo == "no")
                {
                    Console.WriteLine("The account has not been deleted.");
                    waitForAnswer = false;
                }
                else
                {
                    Console.WriteLine("You must choose yes or no.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void Deposit()
        {
            Console.WriteLine("How much do you want to deposit?");
            Account account = CheckCustomerAccountId();

            Console.WriteLine();
            Console.Write("Amount: ");
            string userInput = Console.ReadLine();
            bool result = decimal.TryParse(userInput, out var amount);

            if (result == false || amount < 1)
            {
                Console.WriteLine("The value entered was not correct.");
            }
            else
            {
                account.Balance += amount;
                Console.WriteLine("Success! Total balance: " + account.Balance + ":-");
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void WithDrawal()
        {
            Console.WriteLine("How much do you want to withdrawal?");
            Account account = CheckCustomerAccountId();

            Console.WriteLine();
            Console.WriteLine("Account ID: " + account.Id);
            Console.WriteLine("Current balance: " + account.Balance + ":-");

            Console.WriteLine();
            Console.Write("Amount, ");
            decimal amount = CorrectInputAmount();

            if (amount > account.Balance || amount < 1)
            {
                Console.WriteLine("Not a valid amount to withdrawal," +
                    "\nCurrent balance: " + account.Balance + ":-");
            }
            else
            {
                account.Balance -= amount;
                Console.WriteLine("Success! Total balance left: " + account.Balance + ":-");
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void Transfer()
        {
            Console.Write("FROM, ");
            Account from = CheckCustomerAccountId();

            Console.Write("TO, ");
            Account to = CheckCustomerAccountId();

            Console.Write("How much would you like to transfer? ");
            decimal amount = CorrectInputAmount();

            if (amount > from.Balance)
            {
                Console.WriteLine("Not enough money on account ID " + from.Id + " Current balance: " +
                                  from.Balance + ":-");
            }
            else if (amount < from.Balance)
            {
                to.Balance += amount;
                Console.WriteLine("Amount " + amount + ":- have been transferred to account ID: " +
                                   to.Id);
                from.Balance -= amount;
            }
            else if (amount < 1)
            {
                Console.WriteLine("Not a valid amount.");
            }
            else
            {
                Console.WriteLine("You must type in numbers.");
            }
            Console.WriteLine();
            Console.WriteLine("Press 11 to go back for menu.");
        }

        public void Statistics()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Statistics:");
            string dateNow = DateTime.Now.ToString();
            Console.WriteLine(dateNow);
            int custCount = Customers.Count;
            Console.WriteLine("Amount of customers: " + custCount);

            int accCount = Accounts.Count;
            Console.WriteLine("Amount of accounts: " + accCount);

            decimal totBalance = (from account in Accounts select account.Balance).Sum();

            Console.WriteLine("Total Bank Balance: " + totBalance);
            Console.WriteLine();
        }

        public int GetAvailableCustomerId()
        {
            var findCustomer = (from customer in Customers select customer.Id).Last();

            int newCustomerId = findCustomer + 1;
            return newCustomerId;
        }

        public int GetAvailableAccountId()
        {
            var findAcc = (from account in Accounts select account.Id).Last();

            int newAccountId = findAcc + 1;
            return newAccountId;
        }

        public Customer CheckCustomerId()
        {
            Console.Write("Enter customer ID: ");
            bool option = true;
            while (option)
            {
                string userInput = Console.ReadLine();
                int custId;
                bool result = int.TryParse(userInput, out custId);

                var customer = Customers.SingleOrDefault(a => a.Id == custId);

                if (result == false)
                {
                    Console.WriteLine("Can only type numbers here.");
                    break;
                }
                else if (customer == null)
                {
                    Console.WriteLine("No valid Customer ID.");
                    break;
                }
                else
                {
                    option = false;
                }
                return customer;
            }
            return CheckCustomerId();
        }

        public Account CheckCustomerAccountId()
        {
            Console.Write("Enter account ID: ");
            bool option = true;
            while (option)
            {
                string userInput = Console.ReadLine();
                int accountId;
                bool result = int.TryParse(userInput, out accountId);

                var account = Accounts.SingleOrDefault(a => a.Id == accountId);

                if (result == false)
                {
                    Console.WriteLine("Can only type numbers here.");
                    break;
                }
                else if (account == null)
                {
                    Console.WriteLine("No valid Account ID.");
                    break;
                }
                else
                {
                    option = false;
                }
                return account;
            }
            return CheckCustomerAccountId();
        }

        public decimal CorrectInputAmount()
        {
            bool option = true;
            while (option)
            {
                Console.Write("Enter amount: ");
                string userInput = Console.ReadLine();
                decimal amount;
                bool result = decimal.TryParse(userInput, out amount);

                if (result == false || amount < 1)
                {
                    Console.WriteLine("Not a valid amount.");
                    break;
                }
                else
                {
                    option = false;
                }
                return amount;
            }
            return CorrectInputAmount();
        }
    }
}
