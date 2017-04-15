using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new BookShopContext();
            //ctx.Database.Initialize(true);

            var books = ctx.Books.Take(3).ToList();
            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[2]);
            books[2].RelatedBooks.Add(books[0]);
            ctx.SaveChanges();

            var booksFromQuery = ctx.Books
                .Take(3)
                .Select(s => new
                {
                    name = s.Title,
                    relatedBooks = s.RelatedBooks.Select(n=>n.Title)
                });
            foreach (var book in booksFromQuery)
            {
                Console.WriteLine("--{0}", book.name);
                foreach (var r in book.relatedBooks)
                {
                    Console.WriteLine(r);
                }
            }
        }
    }
}
