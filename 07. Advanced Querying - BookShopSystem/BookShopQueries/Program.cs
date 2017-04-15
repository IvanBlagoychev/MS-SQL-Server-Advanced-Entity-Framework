using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new BookShopContext();


            //======== 1.	Books Titles by Age Restriction ========\\
            //BookTitlesByAgeRestriction(ctx);

            //======== 2.	Golden Books ========\\
            //GoldenBooks(ctx);

            //======== 2.	Golden Books ========\\
            //BooksByPrice(ctx);

            //======== 3.Books by Price ========\\
            //NotReleasedBooks(ctx);

            //======== 4.	Not Released Books ========\\
            //BookTitlesByCategory(ctx);

            //======== 5.	Book Titles by Category ========\\
            //BooksReleasedBeforeDate(ctx);

            //======== 6.	Books Released Before Date ========\\
            //AuthorsSearch(ctx);

            //======== 7.	Authors Search ========\\
            //BookSearch(ctx);

            //======== 8.	Books Search ========\\
            //BookTitleSearch(ctx);

            //======== 9.	Book Titles Search ========\\
            //CountBooks(ctx);

            //======== 10.	Count Books ========\\
            //TotalBookCopies(ctx);

            //======== 11.	Total Book Copies ========\\
            //FindProfit(ctx);

            //======== 12.	Find Profit ========\\
            //MostRecentBooks(ctx);

            //======== 13.	Most Recent Books ========\\
            //IncreaseBookCopies(ctx);

            //======== 14.	Increase Book Copies ========\\
            //RemoveBooks(ctx);

            //======== 15.	Remove Books ========\\
            //StoredProcedure(ctx);
        }

        private static void StoredProcedure(BookShopContext ctx)
        {
            var input = Console.ReadLine().Split(' ');
            string FirstName = input[0];
            string LastName = input[1];
            int count = ctx.Database.SqlQuery<int>("exec BooksCount {0}, {1}", FirstName, LastName).First();
            Console.WriteLine($"{FirstName} {LastName} has written {count} books.");
        }

        private static void RemoveBooks(BookShopContext ctx)
        {
            var books = ctx.Books.Where(c => c.Copies < 4200).ToList();
            foreach (var b in books)
            {
                ctx.Books.Remove(b);
            }
            Console.WriteLine($"{books.Count} books were deleted.");
            ctx.SaveChanges();
        }

        private static void IncreaseBookCopies(BookShopContext ctx)
        {
            var books = ctx.Books.Where(d => d.ReleaseDate > new DateTime(2013, 06, 06));
            int i = 0;
            foreach (var b in books)
            {
                i++;
                b.Copies += 44;
            }
            Console.WriteLine($"{i} books are released after 6 Jun 2013, so {books.Count() * 44} book copies were added.");
        }

        private static void MostRecentBooks(BookShopContext ctx)
        {
            var categories = ctx.Categories.OrderByDescending(c => c.Books.Count);
            foreach (var c in categories)
            {
                Console.WriteLine($"--{c.Name}: {c.Books.Count}");
                foreach (var t in c.Books.OrderByDescending(d => d.ReleaseDate).Take(3))
                {
                    Console.WriteLine($"{t.Title} ({t.ReleaseDate.Value.Year})");
                }
            }
        }

        private static void FindProfit(BookShopContext ctx)
        {
            var categories = ctx.Categories
                .OrderByDescending(s => s.Books.Sum(b => b.Price * b.Copies))
                .ThenBy(c => c.Name)
                .ToList();
            foreach (var c in categories)
                Console.WriteLine($"{c.Name} - ${c.Books.Sum(b => b.Price * b.Copies)}");
        }

        private static void TotalBookCopies(BookShopContext ctx)
        {
            var authors = ctx.Authors.OrderByDescending(o=>o.Books.Sum(c=>c.Copies)).ToList();
            foreach (var a in authors)
            {
                Console.WriteLine($"{a.FirstName} {a.LastName} - {a.Books.Sum(c => c.Copies)}");
            }
        }       

        private static void CountBooks(BookShopContext ctx)
        {
            int num = int.Parse(Console.ReadLine());
            var books = ctx.Books
                .Where(t => t.Title.Length > num)
                .Count();
            Console.WriteLine(books);
        }

        private static void BookTitleSearch(BookShopContext ctx)
        {
            var str = Console.ReadLine().ToLower();
            var books = ctx.Books
                .Where(a => a.Author.LastName.ToLower().StartsWith(str))
                .OrderBy(i => i.Id)
                .Select(t => t.Title);
            Console.WriteLine(string.Join("\n\r", books));
        }

        private static void BookSearch(BookShopContext ctx)
        {
            var str = Console.ReadLine().ToLower();
            var books = ctx.Books
                .Where(t => t.Title.ToLower().Contains(str))
                .Select(t => t.Title)
                .ToList();
            Console.WriteLine(string.Join("\n\r", books));
        }

        private static void AuthorsSearch(BookShopContext ctx)
        {
            Console.Write("Enter letters: ");
            var letters = Console.ReadLine();
            ctx.Authors
                .Where(f => f.FirstName.EndsWith(letters))
                .Select(n => new
                {
                    firstName = n.FirstName,
                    lastName = n.LastName
                })
                .ToList()
                .ForEach(e => Console.WriteLine($"{e.firstName} {e.lastName}"));
        }

        private static void BooksReleasedBeforeDate(BookShopContext ctx)
        {
            var date = Console.ReadLine();
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime NewDate = DateTime.ParseExact(date, "dd-MM-yyyy", provider);
            var books = ctx.Books.Where(d => d.ReleaseDate < NewDate).ToList();
            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} - {b.Type} - {b.Price:f2}");
            }
        }

        private static void BookTitlesByCategory(BookShopContext ctx)
        {
            Console.Write("Enter categories: ");
            string[] categories = Console.ReadLine().Split(' ');
            foreach (var book in ctx.Books)
            {
                if (book.Categories.Any(c => categories.Contains(c.Name.ToLower())))
                {
                    Console.WriteLine(book.Title);
                }
            }

        }

        private static void NotReleasedBooks(BookShopContext ctx)
        {
            Console.Write("Enter a valid year: ");
            int year = int.Parse(Console.ReadLine());
            var books = ctx.Books
                .Where(d => d.ReleaseDate.Value.Year != year)
                .OrderBy(i => i.Id)
                .Select(t => t.Title)
                .ToList();
            Console.WriteLine(string.Join("\n\r", books));
        }

        private static void BooksByPrice(BookShopContext ctx)
        {
            ctx.Books
                .Where(p => p.Price < 5 || p.Price > 40)
                .OrderBy(i => i.Id)
                .Select(s => new
                {
                    title = s.Title,
                    price = s.Price
                }).ToList().ForEach(d => Console.WriteLine($"{d.title} - ${d.price:f2}"));
        }

        private static void GoldenBooks(BookShopContext ctx)
        {
            var books = ctx.Books
                .Where(e => e.Type
                .ToString()
                .Equals("Gold") && e.Copies < 5000)
                .OrderBy(i => i.Id)
                .Select(s => s.Title);

            Console.WriteLine(string.Join("\n\r", books));
        }

        private static void BookTitlesByAgeRestriction(BookShopContext ctx)
        {
            Console.Write("Enter type of restriction: ");
            var restriction = Console.ReadLine().ToLower();
            var books = ctx.Books
                .Where(e => e.Restriction
                .ToString()
                .ToLower()
                .Equals(restriction))
                .Select(t => t.Title)
                .ToList();
            Console.WriteLine(string.Join("\n\r", books));
        }
    }
}
