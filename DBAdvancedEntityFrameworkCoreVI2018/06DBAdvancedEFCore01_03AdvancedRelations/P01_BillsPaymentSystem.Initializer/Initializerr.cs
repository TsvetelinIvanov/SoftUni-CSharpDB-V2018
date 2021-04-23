using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Initializer
{
    public class Initializerr
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InsertUsers(context);
            InsertBankAccounts(context);
            InsertCreditCards(context);
            InsertPaymentMethods(context);            
        }

        private static void InsertUsers(BillsPaymentSystemContext context)
        {
            User[] users = UserInitializer.GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    context.Users.Add(users[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertCreditCards(BillsPaymentSystemContext context)
        {
            CreditCard[] creditCards = CreditCardInitializer.GetCreditCards();

            for (int i = 0; i < creditCards.Length; i++)
            {
                if (IsValid(creditCards[i]))
                {
                    context.CreditCards.Add(creditCards[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertBankAccounts(BillsPaymentSystemContext context)
        {
            BankAccount[] bankAccounts = BankAccountInitializer.GetBankAccounts();

            for (int i = 0; i < bankAccounts.Length; i++)
            {
                if (IsValid(bankAccounts[i]))
                {
                    context.BankAccounts.Add(bankAccounts[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertPaymentMethods(BillsPaymentSystemContext context)
        {
            PaymentMethod[] payments = PaymentMethodInitializer.GetPaymentMethods();

            for (int i = 0; i < payments.Length; i++)
            {
                if (IsValid(payments[i]))
                {
                    context.PaymentMethods.Add(payments[i]);
                }
            }

            context.SaveChanges();
        }


        public static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }
    }
}
