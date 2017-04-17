namespace Tasks1to3
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Tasks1to3.Data;
    using Tasks1to3.Models;

    class Program
    {
        static void Main(string[] args)
        {
            ProductsContext ctx = new ProductsContext();
            //ctx.Database.Initialize(true);

            //============= Task 2 ===============\\
            //Task2ImportData(ctx);


            //============= Task 3 ================\\
            //Task3ProductsInRange(ctx);
            //Task3SuccessfullySoldProducts(ctx);
            //Task3CategoriesByProductsCount(ctx);
            //Task3UsersAndProducts(ctx);
        }

        private static void Task3UsersAndProducts(ProductsContext ctx)
        {
            var users = ctx.Users
                                  .Include("ProductsSold")
                                  .Where(u => u.ProductsSold.Count >= 1)
                                  .OrderByDescending(n => n.ProductsSold.Count)
                                  .ThenBy(l => l.LastName)
                                  .Select(u => new
                                  {
                                      firstName = u.FirstName,
                                      lastname = u.LastName,
                                      age = u.Age,
                                      soldProducts = new
                                      {
                                          count = u.ProductsSold.Count,
                                          products = u.ProductsSold.Select(sp => new
                                          {
                                              name = sp.Name,
                                              price = sp.Price
                                          })
                                      }
                                  });

            var json = JsonConvert.SerializeObject(new { UsersCount = users.Count(), Users = users }, Formatting.Indented);
            File.WriteAllText("../../CategoriesByProductsCount.json", json);
            Console.WriteLine(json);
        }

        private static void Task3CategoriesByProductsCount(ProductsContext ctx)
        {
            var categories = ctx.Categories.OrderBy(n => n.Name)
                            .Select(c => new
                            {
                                category = c.Name,
                                productsCount = c.Products.Count,
                                averagePrice = c.Products.Average(p => p.Price),
                                totalRevenue = c.Products.Sum(p => p.Price)
                            });


            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText("../../CategoriesByProductsCount.json", json);
            Console.WriteLine(json);
        }

        private static void Task3SuccessfullySoldProducts(ProductsContext ctx)
        {
            var users = ctx.Users
                            .Where(u => u.ProductsSold.Count >= 1)
                            .OrderBy(l => l.LastName)
                            .ThenBy(f => f.FirstName)
                            .Select(n => new
                            {
                                firstname = n.FirstName,
                                lastname = n.LastName,
                                productSold = n.ProductsSold.Select(k => new
                                {
                                    name = k.Name,
                                    price = k.Price,
                                    buyerFirstName = k.Buyer.FirstName,
                                    buyerLastName = k.Buyer.LastName
                                })
                            });

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("../../SuccessfullySoldProducts.json", json);
            Console.WriteLine(json);
        }

        private static void Task3ProductsInRange(ProductsContext ctx)
        {
            var products = ctx.Products
                            .Include("Seller")
                            .Where(p => p.Price > 500 && p.Price < 1000)
                            .OrderBy(p => p.Price)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price,
                                seller = p.Seller.FirstName + " " + p.Seller.LastName
                            });

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText("../../ProductsInRange.json", json);
            Console.WriteLine(json);
        }

        private static void Task2ImportData(ProductsContext ctx)
        {

            string usersJson = File.ReadAllText("../../JsonFiles/users.json");
            string productsJson = File.ReadAllText("../../JsonFiles/products.json");
            string categoriesJson = File.ReadAllText("../../JsonFiles/categories.json");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);
            ctx.Users.AddRange(users);
            ctx.SaveChanges();

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
            ctx.Products.AddRange(products);

            int num = 0;
            int usersCount = ctx.Users.Count();
            foreach (Product p in products)
            {
                p.SellerId = (num % usersCount) + 1;
                if (num % 3 != 0)
                {
                    p.BuyerId = (num * 2 % usersCount) + 1;
                }
                num++;
            }
            ctx.Products.AddRange(products);
            ctx.SaveChanges();

            List<Categorie> categories = JsonConvert.DeserializeObject<List<Categorie>>(categoriesJson);

            int num1 = 0;
            int poductsCount = ctx.Products.Count();
            foreach (Categorie c in categories)
            {
                int categoryProductsCount = num1 % 3;
                for (int i = 0; i < categoryProductsCount; i++)
                {
                    c.Products.Add(ctx.Products.Find((num1 % poductsCount) + 1));
                }
                num1++;
            }
            ctx.Categories.AddRange(categories);
            ctx.SaveChanges();
        }
    }
}
