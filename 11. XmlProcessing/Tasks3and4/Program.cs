using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Tasks3and4.Data;
using Tasks3and4.Models;

namespace Tasks3and4
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new ProductShopContext();
            //ctx.Database.Initialize(true);

            //================== Task 3 Import Data =================\\
            //AddUsersToDB(ctx);
            //AddCategoriesToDB(ctx);            
            //AddProductsToDB(ctx);

            //================== Task 4 Query and Export Data =================\\
            //Task4Query1(ctx);
            //Task4Query2(ctx);
            //Task4Query3(ctx);
            //Task4Query4(ctx);
        }

        private static void Task4Query4(ProductShopContext ctx)
        {
            var users = ctx.Users
                            .Where(p => p.ProductsSold.Count >= 1)
                            .OrderByDescending(p => p.ProductsSold.Count).ThenBy(l => l.LastName).Select(u => new
                            {
                                firstname = u.FirstName,
                                lastname = u.LastName,
                                age = u.Age,
                                product = u.ProductsSold.Select(p => new
                                {
                                    name = p.Name,
                                    price = p.Price
                                })
                            });
            XDocument doc = new XDocument();
            doc.Add(new XElement("users", new XAttribute("count", users.Count())));
            foreach (var u in users)
            {
                if (u.age != null && u.firstname != null)
                {
                    XElement element = new XElement("user",
                             new XAttribute("first-name", u.firstname ?? ""),
                             new XAttribute("last-name", u.lastname),
                             new XAttribute("age", u.age),
                             new XElement("sold-products", new XAttribute("count", u.product.Count())));
                    foreach (var p in u.product)
                    {
                        element.Element("sold-products").Add(new XElement("product",
                               new XElement("name", p.name),
                               new XElement("price", p.price)));
                    }
                    doc.Element("users").Add(element);
                }
                else if (u.age == null && u.firstname != null)
                {
                    XElement element = new XElement("user",
                             new XAttribute("first-name", u.firstname ?? ""),
                             new XAttribute("last-name", u.lastname),
                             new XElement("sold-products", new XAttribute("count", u.product.Count())));
                    foreach (var p in u.product)
                    {
                        element.Element("sold-products").Add(new XElement("product",
                               new XElement("name", p.name),
                               new XElement("price", p.price)));
                    }
                    doc.Element("users").Add(element);
                }

                else if (u.age != null && u.firstname == null)
                {
                    XElement element = new XElement("user",
                             new XAttribute("last-name", u.lastname),
                             new XAttribute("age", u.age),
                             new XElement("sold-products", new XAttribute("count", u.product.Count())));
                    foreach (var p in u.product)
                    {
                        element.Element("sold-products").Add(new XElement("product",
                               new XElement("name", p.name),
                               new XElement("price", p.price)));
                    }
                    doc.Element("users").Add(element);
                }

                else if (u.age == null && u.firstname == null)
                {
                    XElement element = new XElement("user",
                             new XAttribute("last-name", u.lastname),
                             new XElement("sold-products", new XAttribute("count", u.product.Count())));
                    foreach (var p in u.product)
                    {
                        element.Element("sold-products").Add(new XElement("product",
                               new XElement("name", p.name),
                               new XElement("price", p.price)));
                    }
                    doc.Element("users").Add(element);
                }

            }
            doc.Save("../../XmlResultFiles/UsersAndProducts.xml");
        }

        private static void Task4Query3(ProductShopContext ctx)
        {
            var categories = ctx.Categories.OrderBy(p => p.Products.Count).Select(c => new
            {
                name = c.Name,
                productsCount = c.Products.Count,
                averagePrice = c.Products.Average(p => p.Price),
                totalRevenue = c.Products.Sum(s => s.Price)
            });
            XDocument doc = new XDocument();
            doc.Add(new XElement("categories"));
            foreach (var c in categories)
            {
                doc.Element("categories").Add(
                    new XElement("category",
                    new XAttribute("name", c.name),
                    new XElement("products-count", c.productsCount),
                    new XElement("average-price", c.averagePrice),
                    new XElement("total-revenue", c.totalRevenue)));
            }
            doc.Save("../../XmlResultFiles/CategoriesByProductsCount.xml");
        }

        private static void Task4Query2(ProductShopContext ctx)
        {
            XDocument resDoc = new XDocument();
            resDoc.Add(new XElement("users"));

            var users = ctx.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderBy(l => l.LastName)
                .ThenBy(f => f.FirstName)
                .Select(d => new
                {
                    firstName = d.FirstName,
                    lastName = d.LastName,
                    product = d.ProductsSold.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price
                    })
                });

            foreach (var u in users)
            {
                XElement xel = new XElement(
                       new XElement("user",
                       new XAttribute("first-name", u.firstName ?? ""),
                       new XAttribute("last-name", u.lastName),
                       new XElement("sold-products")));
                foreach (var p in u.product)
                {
                    xel.Element("sold-products").Add(new XElement("product",
                           new XElement("name", p.name),
                           new XElement("price", p.price)));
                }
                resDoc.Element("users").Add(xel);
            }

            resDoc.Save("../../XmlResultFiles/SoldProducts.xml");
        }

        private static void Task4Query1(ProductShopContext ctx)
        {
            var products = ctx.Products
                            .Where(p => (p.Price >= 1000 && p.Price <= 2000) && p.Buyer != null)
                            .OrderBy(p => p.Price).Select(s => new
                            {
                                name = s.Name,
                                price = s.Price,
                                buyer = s.Buyer.FirstName + " " + s.Buyer.LastName
                            });
            XDocument newDoc = new XDocument();
            newDoc.Add(new XElement("products"));
            foreach (var p in products)
            {
                newDoc.Element("products").Add(
                    new XElement("product",
                    new XAttribute("name", p.name),
                    new XAttribute("price", p.price),
                    new XAttribute("buyer", p.buyer)));
            }
            newDoc.Save("../../XmlResultFiles/ProductsInRange.xml");
        }

        private static void AddProductsToDB(ProductShopContext ctx)
        {
            XDocument xmlData = XDocument.Load("../../XmlFiles/products.xml");
            var products = xmlData.Root.Elements();
            var users = ctx.Users.ToList();
            var categories = ctx.Categories.ToList();
            Random rnd = new Random();

            foreach (var product in products)
            {
                Product newProduct = new Product();
                User seller = users[rnd.Next(users.Count)];
                newProduct.Name = product.Element("name").Value;
                newProduct.Price = decimal.Parse(product.Element("price").Value);
                newProduct.Seller = seller;
                int num = rnd.Next(categories.Count);
                for (int i = 0; i < 2; i++)
                {
                    newProduct.Categories.Add(categories[num]);
                }
                ctx.Products.Add(newProduct);
            }
            ctx.SaveChanges();
            var productFromDb = ctx.Products.ToList();
            for (int i = 0; i < productFromDb.Count; i += 2)
            {
                User Buyer = users[rnd.Next(users.Count)];
                productFromDb[i].Buyer = Buyer;
            }
            ctx.SaveChanges();
            Console.WriteLine("Products successfully added to Database.");
        }

        private static void AddCategoriesToDB(ProductShopContext ctx)
        {
            XDocument xmlData = XDocument.Load("../../XmlFiles/categories.xml");
            var categories = xmlData.Root.Elements();
            foreach (var category in categories)
            {
                Category newCategory = new Category()
                {
                    Name = category.Element("name").Value
                };
                ctx.Categories.Add(newCategory);
            }
            ctx.SaveChanges();
            Console.WriteLine("All categories successfully added to Database.");
        }

        private static void AddUsersToDB(ProductShopContext ctx)
        {
            XDocument newDoc = XDocument.Load("../../XmlFiles/users.xml");
            var users = newDoc.Root.Elements();

            foreach (var u in users)
            {
                User newUser = new User();
                var str = u.ToString();
                if (str.Contains("first-name") && str.Contains("age"))
                {
                    newUser.FirstName = u.Attribute("first-name").Value;
                    newUser.LastName = u.Attribute("last-name").Value;
                    newUser.Age = int.Parse(u.Attribute("age").Value);
                }
                else if (str.Contains("first-name") && !str.Contains("age"))
                {
                    newUser.FirstName = u.Attribute("first-name").Value;
                    newUser.LastName = u.Attribute("last-name").Value;
                }
                else
                {
                    newUser.LastName = u.Attribute("last-name").Value;
                }

                ctx.Users.Add(newUser);
            }
            ctx.SaveChanges();
            Console.WriteLine("Users successfully added to database.");
        }
    }
}
