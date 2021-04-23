using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Initializer
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            CreditCard[] creditCards = new CreditCard[]
            {
                new CreditCard() {Limit = 1000, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 1, 6, 30, 15)},
                new CreditCard() {Limit = 1500, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 2, 6, 30, 15)},
                new CreditCard() {Limit = 400, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 3, 6, 30, 15)},
                new CreditCard() {Limit = 7000, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 4, 6, 30, 15)},
                new CreditCard() {Limit = 900, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 5, 6, 30, 15)},
                new CreditCard() {Limit = 100, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 6, 6, 30, 15)},
                new CreditCard() {Limit = 700, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 7, 6, 30, 15)},
                new CreditCard() {Limit = 800, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 8, 6, 30, 15)},
                new CreditCard() {Limit = 9500, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 9, 6, 30, 15)},
                new CreditCard() {Limit = 1300, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 10, 6, 30, 15)},
                new CreditCard() {Limit = 7800, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 11, 6, 30, 15)},
                new CreditCard() {Limit = 8900, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 1, 12, 6, 30, 15)},
                new CreditCard() {Limit = 9900, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 9, 1, 6, 30, 15)},
                new CreditCard() {Limit = 7100, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 8, 1, 6, 30, 15)},
                new CreditCard() {Limit = 1600, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 7, 1, 6, 30, 15)},
                new CreditCard() {Limit = 9700, MoneyOwed = 0, ExpirationDate = new DateTime(2021, 6, 1, 6, 30, 15)}
            };

            return creditCards;
        }
    }
}
