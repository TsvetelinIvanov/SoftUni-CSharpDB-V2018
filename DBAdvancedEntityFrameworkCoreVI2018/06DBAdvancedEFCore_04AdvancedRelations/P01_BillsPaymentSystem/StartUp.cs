using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Initializer;
using System;
using System.Globalization;
using System.Linq;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //Initializerr.Seed(context);

                User user = GetUser(context);
                PrintUserInfo(user);
                PayBills(user, 500);
                context.SaveChanges();
            }
        }

        private static User GetUser(BillsPaymentSystemContext context)
        {
            int userId = int.Parse(Console.ReadLine());

            User user = null;

            while (true)
            {
                user = context.Users.Where(u => u.UserId == userId)
                 .Include(u => u.PaymentMethods).ThenInclude(pm => pm.BankAccount)
                 .Include(u => u.PaymentMethods).ThenInclude(pm => pm.CreditCard)
                 .FirstOrDefault();

                if (user == null)
                {
                    Console.WriteLine($"User with id {userId} not found!");
                    userId = int.Parse(Console.ReadLine());
                    continue;
                }

                break;
            }

            return user;
        }

        private static void PrintUserInfo(User user)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine("Bank Accounts:");
            BankAccount[] bankAccounts = user.PaymentMethods.Where(pm => pm.BankAccount != null)
                .Select(pm => pm.BankAccount).ToArray();
            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine($"-- ID: {bankAccount.BankAccountId}");
                Console.WriteLine($"--- Balance: {bankAccount.Balance:f2}");
                Console.WriteLine($"--- Bank: {bankAccount.BankName}");
                Console.WriteLine($"--- SWIFT: {bankAccount.SWIFTCode}");
            }
            Console.WriteLine("Credit Cards:");
            CreditCard[] creditCards = user.PaymentMethods.Where(pm => pm.CreditCard != null)
               .Select(pm => pm.CreditCard).ToArray();
            foreach (var creditCard in creditCards)
            {
                Console.WriteLine($"-- ID {creditCard.CreditCardId}");
                Console.WriteLine($"--- Limit: {creditCard.Limit:f2}");
                Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                Console.WriteLine($"--- Limit Left: {creditCard.LimitLeft:f2}");
                Console.WriteLine($"--- Expiration Date: " +
                    $"{creditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
            }
        }

        private static void PayBills(User user, decimal amount)
        {
            decimal bankAccountsSum = user.PaymentMethods.Where(pm => pm.BankAccount != null)
                .Sum(pm => pm.BankAccount.Balance);
            decimal creditCartsSum = user.PaymentMethods.Where(pm => pm.CreditCard != null)
                .Sum(pm => pm.CreditCard.LimitLeft);

            decimal totalSum = bankAccountsSum + creditCartsSum;

            if (totalSum >= amount)
            {
                BankAccount[] bankAccounts = user.PaymentMethods.Where(pm => pm.BankAccount != null)
                    .Select(pm => pm.BankAccount).OrderBy(pm => pm.BankAccountId).ToArray();
                foreach (var bankAccount in bankAccounts)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= bankAccount.Balance;
                        bankAccount.Withdraw(bankAccount.Balance);
                    }

                    if (amount == 0)
                    {
                        Console.WriteLine("Bills were successfully payed.");
                        return;
                    }

                    CreditCard[] creditCards = user.PaymentMethods.Where(pm => pm.CreditCard != null)
                        .Select(pm => pm.CreditCard).OrderBy(pm => pm.CreditCardId).ToArray();
                    foreach (var creditCard in creditCards)
                    {
                        if (creditCard.LimitLeft >= amount)
                        {
                            creditCard.Withdraw(amount);
                            amount = 0;
                        }
                        else
                        {
                            amount -= creditCard.LimitLeft;
                            creditCard.Withdraw(creditCard.LimitLeft);
                        }

                        if (amount == 0)
                        {
                            Console.WriteLine("Bills were successfully payed.");
                            return;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Insuficient funds!");
            }
        }                
    }
}