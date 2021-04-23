using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Initializer
{
    public class PaymentMethodInitializer
    {
        public static PaymentMethod[] GetPaymentMethods()
        {
            PaymentMethod[] paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod() {UserId = 1, Type = PaymentMethodType.BankAccount, BankAccountId = 1},
                new PaymentMethod() {UserId = 2, Type = PaymentMethodType.CreditCard, CreditCardId = 1},
                new PaymentMethod() {UserId = 3, Type = PaymentMethodType.BankAccount, BankAccountId = 2},
                new PaymentMethod() {UserId = 4, Type = PaymentMethodType.CreditCard, CreditCardId = 2},
                new PaymentMethod() {UserId = 5, Type = PaymentMethodType.BankAccount, BankAccountId = 3},
                new PaymentMethod() {UserId = 6, Type = PaymentMethodType.CreditCard, CreditCardId = 3},
                new PaymentMethod() {UserId = 7, Type = PaymentMethodType.BankAccount, BankAccountId = 4},
                new PaymentMethod() {UserId = 8, Type = PaymentMethodType.CreditCard, CreditCardId = 4},
                new PaymentMethod() {UserId = 9, Type = PaymentMethodType.BankAccount, BankAccountId = 5},
                new PaymentMethod() {UserId = 10, Type = PaymentMethodType.CreditCard, CreditCardId = 5},
                new PaymentMethod() {UserId = 11, Type = PaymentMethodType.BankAccount, BankAccountId = 6},
                new PaymentMethod() {UserId = 12, Type = PaymentMethodType.CreditCard, CreditCardId = 6},
                new PaymentMethod() {UserId = 13, Type = PaymentMethodType.BankAccount, BankAccountId = 7},
                new PaymentMethod() {UserId = 14, Type = PaymentMethodType.CreditCard, CreditCardId = 7},
                new PaymentMethod() {UserId = 15, Type = PaymentMethodType.BankAccount, BankAccountId = 8},
                new PaymentMethod() {UserId = 16, Type = PaymentMethodType.CreditCard, CreditCardId = 8},
                new PaymentMethod() {UserId = 17, Type = PaymentMethodType.BankAccount, BankAccountId = 9},
                new PaymentMethod() {UserId = 18, Type = PaymentMethodType.CreditCard, CreditCardId = 9},
                new PaymentMethod() {UserId = 19, Type = PaymentMethodType.BankAccount, BankAccountId = 10},
                new PaymentMethod() {UserId = 21, Type = PaymentMethodType.CreditCard, CreditCardId = 10}
            };

            return paymentMethods;
        }
    }
}
