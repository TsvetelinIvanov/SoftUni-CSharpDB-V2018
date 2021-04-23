using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Initializer
{
    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User() {FirstName = "Angel", LastName = "Angelov", Email = "angel@abv.bg", Password = "a0123456"},
                new User() {FirstName = "Bogdan", LastName = "Bogdanov", Email = "bogdan@abv.bg", Password = "b0123456"},
                new User() {FirstName = "Vasil", LastName = "Vasilev", Email = "vasko@abv.bg", Password = "v0123456"},
                new User() {FirstName = "Ginka", LastName = "Ginkova", Email = "ginka@abv.bg", Password = "g0123456"},
                new User() {FirstName = "Daniela", LastName = "Danchova", Email = "dana@abv.bg", Password = "d0123456"},
                new User() {FirstName = "Emil", LastName = "Emilov", Email = "emil@abv.bg", Password = "e0123456"},
                new User() {FirstName = "Zornica", LastName = "Zorova", Email = "zory@abv.bg", Password = "z0123456"},
                new User() {FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@abv.bg", Password = "i0123456"},
                new User() {FirstName = "Yordanka", LastName = "Yordanova", Email = "yordankal@abv.bg", Password = "y0123456"},
                new User() {FirstName = "Kaloyan", LastName = "Kaloyanchev", Email = "kacho@abv.bg", Password = "k0123456"},
                new User() {FirstName = "Lyuba", LastName = "Lybenova", Email = "love@abv.bg", Password = "l0123456"},
                new User() {FirstName = "Mihail", LastName = "Mihailov", Email = "misho@abv.bg", Password = "m0123456"},
                new User() {FirstName = "Nadia", LastName = "Naidenova", Email = "nada@abv.bg", Password = "n0123456"},
                new User() {FirstName = "Ognyan", LastName = "Ognyanov", Email = "ogi@abv.bg", Password = "o0123456"},
                new User() {FirstName = "Petar", LastName = "Petrov", Email = "pesho@abv.bg", Password = "p0123456"},
                new User() {FirstName = "Rumyana", LastName = "Rumenova", Email = "rumi@abv.bg", Password = "r0123456"},
                new User() {FirstName = "Stoyan", LastName = "Stoyanov", Email = "stoine@abv.bg", Password = "s0123456"},
                new User() {FirstName = "Ulyana", LastName = "Ulyanova", Email = "ulya@abv.bg", Password = "u0123456"},
                new User() {FirstName = "Fahradin", LastName = "Fahradinov", Email = "fahi@abv.bg", Password = "f0123456"},
                new User() {FirstName = "Hiperion", LastName = "Hiperionov", Email = "hipy@abv.bg", Password = "h0123456"},
                new User() {FirstName = "Quarter", LastName = "Quarterov", Email = "qu@abv.bg", Password = "q0123456"},
                new User() {FirstName = "Ximo", LastName = "Xiovski", Email = "ximo@abv.bg", Password = "x0123456"}
            };

            return users;
        }
    }
}
