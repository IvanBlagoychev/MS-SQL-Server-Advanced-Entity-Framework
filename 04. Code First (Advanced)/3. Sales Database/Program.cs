using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Sales_Database
{
    class Program
    {
        static void Main(string[] args)
        {

            //------------- Tasks 4,5,6,7 are inside task 3, check classes inside Models folder and Migrations folder-----------------\\
            
            var context = new SalesContext();
            Customer c = new Customer();

            Product p = new Product();
            p.Name = "Product";
            p.Description = "Description";
            context.Products.Add(p);
            Console.WriteLine(context.Products.First().Name);
            context.SaveChanges();
         }

       // private static void AddSampleData(SalesContext context)
    //    {
    //        Product truck = new Product()
    //        {
    //            Name = "Fire truck",
    //            Quantity = 5,
    //            Price = 3.00m
    //        };
    //        Product speakers = new Product()
    //        {
    //            Name = "Creative",
    //            Quantity = 1,
    //            Price = 120.00m
    //        };
    //        Product laptop = new Product()
    //        {
    //            Name = "Asus Rog",
    //            Quantity = 2,
    //            Price = 5000.00m
    //        };

    //        Customer pesho = new Customer()
    //        {
    //            Name = "Pesho",
    //            Email = "pesho@SoftUniLegends.bg",
    //            CreditCardNumber = "djndjncdnc7887"
    //        };
    //        Customer georgi = new Customer()
    //        {
    //            Name = "Georgi",
    //            Email = "georgi@SoftUniTrainers.bg",
    //            CreditCardNumber = "hsxbhgsdbd7726382"
    //        };
    //        Customer ivan = new Customer()
    //        {
    //            Name = "Ivan",
    //            Email = "ivan@SoftUniStudents.bg",
    //            CreditCardNumber = "asjhhcndcndk7273982"
    //        };
    //        StoreLocation sofia = new StoreLocation()
    //        {
    //            LocationName = "Sofia"
    //        };
    //        StoreLocation varna = new StoreLocation()
    //        {
    //            LocationName = "Varna"
    //        };
    //        StoreLocation plovdiv = new StoreLocation()
    //        {
    //            LocationName = "Plovdiv"
    //        };

    //        Sale truckSale = new Sale()
    //        {
    //            Product = truck,
    //            Customer = pesho,
    //            StoreLocation = plovdiv,
    //            Date = DateTime.Now
    //        };
    //        Sale speakersSale = new Sale()
    //        {
    //            Product = speakers,
    //            Customer = georgi,
    //            StoreLocation = sofia,
    //            Date = DateTime.Now
    //        };
    //        Sale laptopSale = new Sale()
    //        {
    //            Product = laptop,
    //            Customer = ivan,
    //            StoreLocation = varna,
    //            Date = DateTime.Now
    //        };

    //        context.Sales.Add(truckSale);
    //        context.Sales.Add(speakersSale);
    //        context.Sales.Add(laptopSale);
    //        context.SaveChanges();
    //    }
    }
}
