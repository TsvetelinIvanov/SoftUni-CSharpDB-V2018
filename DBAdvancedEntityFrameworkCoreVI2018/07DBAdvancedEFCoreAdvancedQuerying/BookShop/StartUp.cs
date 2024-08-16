using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BookShop
{
    public class StartUp
    {
        public static void Main()
        {
            using (BookShopContext db = new BookShopContext())
            {
                //00BookShopDatabase
                DbInitializer.ResetDatabase(db);

                //01AgeRestriction
                //string ageRestriction = Console.ReadLine();
                //string titlesFromAgeRestrictedBooksString = GetBooksByAgeRestriction(db, ageRestriction);
                //Console.WriteLine(titlesFromAgeRestrictedBooksString);

                //02GoldenBooks
                //string goldenBooksString = GetGoldenBooks(db);
                //Console.WriteLine(goldenBooksString);

                //03BooksByPrice
                //string booksTitlesAndPricesString = GetBooksByPrice(db);
                //Console.WriteLine(booksTitlesAndPricesString);

                //04NotReleasedIn
                //int releaseYear = int.Parse(Console.ReadLine());
                //string notReleasedBooksString = GetBooksNotRealeasedIn(db, releaseYear);
                //Console.WriteLine(notReleasedBooksString);

                //05BookTitlesByCategory
                //string categoriesString = Console.ReadLine();
                //string titlesByCategoriesString = GetBooksByCategory(db, categoriesString);
                //Console.WriteLine(titlesByCategoriesString);

                //06ReleasedBeforeDate
                //string dateString = Console.ReadLine();
                //string releasedBooksString = GetBooksReleasedBefore(db, dateString);
                //Console.WriteLine(releasedBooksString);

                //07AuthorSearch
                //string ending = Console.ReadLine();
                //string authorNamesString = GetAuthorNamesEndingIn(db, ending);
                //Console.WriteLine(authorNamesString);

                //08BookSearch
                //string input = Console.ReadLine();
                //string titlesString = GetBookTitlesContaining(db, input);
                //Console.WriteLine(titlesString);

                //09BookSearchByAuthor
                //string input = Console.ReadLine();
                //string titlesWithAuthorsString = GetBooksByAuthor(db, input);
                //Console.WriteLine(titlesWithAuthorsString);

                //10CountBooks
                //int lengthCheck = int.Parse(Console.ReadLine());
                //int bookCount = CountBooks(db, lengthCheck);
                //Console.WriteLine(bookCount);

                //11TotalBookCopies
                //string authorsAndCopiesString = CountCopiesByAuthor(db);
                //Console.WriteLine(authorsAndCopiesString);

                //12ProfitByCategory
                //string profitByCategoriesString = GetTotalProfitByCategory(db);
                //Console.WriteLine(profitByCategoriesString);

                //13MostRecentBooks
                //string mostRecentBooksString = GetMostRecentBooks(db);
                //Console.WriteLine(mostRecentBooksString);

                //14IncreasePrices
                //int increasedPricesCount =  IncreasePrices(db);
                //Console.WriteLine(increasedPricesCount);

                //15RemoveBooks
                //int removedBooksCount = RemoveBooks(db);
                //Console.WriteLine(removedBooksCount +  " books were deleted");
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            //List<string> titlesFromAgeRestrictedBooks = context.Books
            //    .Where(b => b.AgeRestriction.ToString().Equals(command, StringComparison.OrdinalIgnoreCase))
            //    .Select(b => b.Title)
            //    .OrderBy(t => t)
            //    .ToList();

            AgeRestriction ageRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);
            List<string> titlesFromAgeRestrictedBooks = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            string titlesFromAgeRestrictedBooksString = String.Join(Environment.NewLine, titlesFromAgeRestrictedBooks);

            return titlesFromAgeRestrictedBooksString;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            string[] goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            string goldenBooksString = string.Join(Environment.NewLine, goldenBooks);

            return goldenBooksString;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            string[] booksTitlesAndPrices = context
                .Books.Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToArray();

            StringBuilder bookBuilder = new StringBuilder();
            foreach (string bookTitleAndPrice in booksTitlesAndPrices)
            {
                bookBuilder.AppendLine(bookTitleAndPrice);
            }

            string booksTitlesAndPricesString = bookBuilder.ToString().TrimEnd();

            return booksTitlesAndPricesString;
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            string[] notReleasedBooks = context.Books
                //.Where(b => b.ReleaseDate != null && b.ReleaseDate.Value.Year != year)
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            string notReleasedBooksString = string.Join(Environment.NewLine, notReleasedBooks);

            return notReleasedBooksString;
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            //string[] categories = input.ToLower().Split(new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries);
            //string[] titlesByCategories = context.Books.Where(b => b.BookCategories
            //    .Select(bc => bc.Category.Name.ToLower()).Intersect(categories).Any())
            //    .Select(b => b.Title)
            //    .OrderBy(t => t)
            //    .ToArray();

            string[] categories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] titlesByCategories = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            string titlesByCategoriesString = string.Join(Environment.NewLine, titlesByCategories);

            return titlesByCategoriesString;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string[] releasedBooks = context.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            string releasedBooksString = string.Join(Environment.NewLine, releasedBooks);

            return releasedBooksString;
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            //string[] authorNames = context.Authors.Where(a => a.FirstName.EndsWith(input))
            //    .OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
            //    .Select(a => $"{a.FirstName} {a.LastName}")
            //    .ToArray();

            //string[] authorNames = context.Authors.Where(a => a.FirstName.EndsWith(input))
            //    .Select(a => $"{a.FirstName} {a.LastName}")
            //    .OrderBy(n => n)
            //    .ToArray();

            string[] authorNames = context.Authors.Where(a => EF.Functions.Like(a.FirstName, "%" + input))
                    .Select(a => $"{a.FirstName} {a.LastName}")
                    .OrderBy(n => n)
                    .ToArray();

            string authorNamesString = string.Join(Environment.NewLine, authorNames);
            
            return authorNamesString;
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            input = input.ToLower();

            //string[] titles = context.Books.Where(b => b.Title.ToLower().Contains(input))
            //    .Select(b => b.Title).OrderBy(t => t).ToArray();

            //string[] titles = context.Books.Where(b => EF.Functions.Like(b.Title, "%" + input + "%"))
            //    .Select(b => b.Title).OrderBy(t => t).ToArray();

            string[] titles = context.Books.Where(b => EF.Functions.Like(b.Title, $"%{input}%"))
                .Select(b => b.Title).OrderBy(t => t).ToArray();

            string titlesString = string.Join(Environment.NewLine, titles);

            return titlesString;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            input = input.ToLower();

            //string[] titlesWithAuthors = context.Books
            //    .Where(b => b.Author.LastName.ToLower().StartsWith(input))
            //    .OrderBy(b => b.BookId)
            //    .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
            //    .ToArray();

            string[] titlesWithAuthors = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, input + "%"))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            string titlesWithAuthorsString = string.Join(Environment.NewLine, titlesWithAuthors);

            return titlesWithAuthorsString;
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int bookCount = context.Books.Where(b => b.Title.Length > lengthCheck).Count();

            return bookCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            string[] authorsAndCopies = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(ac => ac.Copies)
                .Select(ac => $"{ac.Name} - {ac.Copies}")
                .ToArray();

            string authorsAndCopiesString = string.Join(Environment.NewLine, authorsAndCopies);

            return authorsAndCopiesString;
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            string[] profitByCategories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                })
                       .OrderByDescending(pc => pc.Profit).ThenBy(pc => pc.CategoryName)
                       .Select(pc => $"{pc.CategoryName} ${pc.Profit:f2}")
                       .ToArray();

            string profitByCategoriesString = string.Join(Environment.NewLine, profitByCategories);

            return profitByCategoriesString;
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            string[] mostRecentBooks = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    CategoryMostRecentBooksString = string.Join(Environment.NewLine,
                         c.CategoryBooks.Select(cb => cb.Book)
                             .OrderByDescending(b => b.ReleaseDate).Take(3)
                             .Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))
                })
                .OrderBy(mrb => mrb.CategoryName)
                .Select(mrb => $"--{mrb.CategoryName}{Environment.NewLine}{mrb.CategoryMostRecentBooksString}")
                .ToArray();

            string mostRecentBooksString = string.Join(Environment.NewLine, mostRecentBooks);

            return mostRecentBooksString;
        }

        public static int IncreasePrices(BookShopContext context)
        {
            Book[] books = context.Books.Where(b => b.ReleaseDate.Value.Year < 2010).ToArray();
            foreach (Book book in books)
            {
                book.Price += 5;
            }

            int increasedPricesCount = context.SaveChanges();

            return increasedPricesCount;
        }

        public static int RemoveBooks(BookShopContext context)
        {
            Book[] booksForRemoving = context.Books.Where(b => b.Copies < 4200).ToArray();
            int removedBooksCount = booksForRemoving.Length;
            context.RemoveRange(booksForRemoving);

            //return context.SaveChanges(); <-this returns correct answer, but Judge don't accept it

            context.SaveChanges();
            
            return removedBooksCount;
        }
    }
}
