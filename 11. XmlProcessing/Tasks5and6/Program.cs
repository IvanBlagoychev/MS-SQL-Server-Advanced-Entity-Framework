using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tasks5and6.Data;
using Tasks5and6.Models;

namespace Tasks5and6
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CarDealerContext();
            //ctx.Database.Initialize(true);

            //================ Task 5 Car Dealer Import Data ================\\
            //AddSuppliersToDatabase(ctx);
            //AddPartsToDatabase(ctx);
            //AddCarsToDatabase(ctx);
            //AddCustomersToDatabase(ctx);
            //AddSalesToDatabase(ctx);


            //================ Task 6 Car Dealer Query and Export Data ================\\
            //Query1Cars(ctx);
            //Query2CarsFromFerrary(ctx);
            //Query3LocalSuppliers(ctx);
            //Query4CarsWithTheirListofParts(ctx);
            //Query5TotalSalesByCustomer(ctx);
            //SalesWithAppliedDiscount(ctx);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext ctx)
        {
            XDocument salesXml = new XDocument();
            salesXml.Add(new XElement("sales"));
            var sales = ctx.Sales
                .Where(s => s.Discount != 0)
                .Select(c => new
                {
                    make = c.Car.Make,
                    model = c.Car.Model,
                    distance = c.Car.TravelledDistance,
                    customer = c.Customer.Name,
                    discount = c.Discount,
                    price = c.Car.Parts.Sum(r => r.Price),
                    priceWithDiscount = c.Car.Parts.Sum(r => r.Price) - (c.Car.Parts.Sum(r => r.Price) * c.Discount)
                });
            foreach (var s in sales)
            {
                XElement sale = new XElement("sale",
                    new XElement("car",
                    new XAttribute("make", s.make),
                    new XAttribute("model", s.model),
                    new XAttribute("travelled-distance", s.distance)),
                    new XElement("customer-name", s.customer),
                    new XElement("discount", s.discount),
                    new XElement("price", s.price),
                    new XElement("price-with-discount", s.priceWithDiscount));
                salesXml.Element("sales").Add(sale);
            }
            salesXml.Save("../../XmlFilesResult/SalesWithAppliedDiscount.xml");
        }
        private static void Query5TotalSalesByCustomer(CarDealerContext ctx)
        {
            XDocument customersXml = new XDocument();
            customersXml.Add(new XElement("customers"));
            var customers = ctx.Customers
                .Where(b => b.Sales.Count >= 1)
                .OrderByDescending(s => s.Sales.Sum(p => p.Car.Parts.Sum(f => f.Price)))
                .ThenByDescending(b => b.Sales.Count)
                .Select(c => new
                {
                    names = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(p => p.Car.Parts.Sum(f => f.Price))
                });
            foreach (var c in customers)
            {
                XElement customer = new XElement("customer",
                    new XAttribute("full-name", c.names),
                    new XAttribute("bought-cars", c.boughtCars),
                    new XAttribute("spent-money", c.spentMoney));
                customersXml.Element("customers").Add(customer);
            }
            customersXml.Save("../../XmlFilesResult/TotalSalesByCustomer.xml");
        }
        private static void Query4CarsWithTheirListofParts(CarDealerContext ctx)
        {
            XDocument carsXml = new XDocument();
            carsXml.Add(new XElement("cars"));
            var cars = ctx.Cars.Select(c => new
            {
                make = c.Make,
                model = c.Model,
                travelledDistance = c.TravelledDistance,
                parts = c.Parts.Select(p => new
                {
                    name = p.Name,
                    price = p.Price
                })
            });
            foreach (var c in cars)
            {
                XElement car = new XElement("car",
                    new XAttribute("make", c.make),
                    new XAttribute("model", c.model),
                    new XAttribute("travelled-distance", c.travelledDistance));
                foreach (var p in c.parts)
                {
                    car.Add(new XElement("parts", new XElement("part",
                        new XAttribute("name", p.name),
                        new XAttribute("price", p.price))));
                }
                carsXml.Element("cars").Add(car);
            }
            carsXml.Save("../../XmlFilesResult/CarsWithTheirListOfParts.xml");
        }
        private static void Query3LocalSuppliers(CarDealerContext ctx)
        {
            XDocument suppliersXml = new XDocument();
            suppliersXml.Add(new XElement("suppliers"));
            var suppliers = ctx.Suppliers
                .Where(a => a.IsImporter == false)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    partsCount = s.Parts.Count
                });
            foreach (var s in suppliers)
            {
                suppliersXml.Element("suppliers").Add(
                    new XElement("supplier",
                    new XAttribute("id", s.id),
                    new XAttribute("name", s.name),
                    new XAttribute("part-count", s.partsCount)));
            }
            suppliersXml.Save("../../XmlFilesResult/LocalSuppliers.xml");
        }
        private static void Query2CarsFromFerrary(CarDealerContext ctx)
        {
            XDocument FerrariXml = new XDocument();
            FerrariXml.Add(new XElement("cars"));
            var ferrari = ctx.Cars
                .Where(m => m.Make == "Ferrari")
                .OrderBy(m => m.Model)
                .ThenByDescending(d => d.TravelledDistance)
                .ToList();
            foreach (var f in ferrari)
            {
                FerrariXml.Element("cars").Add(
                    new XElement("car",
                    new XAttribute("id", f.Id),
                    new XAttribute("model", f.Model),
                    new XAttribute("travelled-distance", f.TravelledDistance)));
            }
            FerrariXml.Save("../../XmlFilesResult/CarsFromFerrari.xml");
        }
        private static void Query1Cars(CarDealerContext ctx)
        {
            XDocument carsResult = new XDocument();
            carsResult.Add(new XElement("cars"));
            var cars = ctx.Cars
                .Where(d => d.TravelledDistance > 2000000)
                .OrderBy(m => m.Make)
                .ThenBy(m => m.Model).ToList();
            foreach (var c in cars)
            {
                carsResult.Element("cars").Add(
                    new XElement("car",
                    new XElement("make", c.Make),
                    new XElement("model", c.Model),
                    new XElement("travelled-distance", c.TravelledDistance)));
            }
            carsResult.Save("../../XmlFilesResult/Cars.xml");
        }
        private static void AddSalesToDatabase(CarDealerContext ctx)
        {
            decimal[] discounts = new decimal[] { 0.00m, 0.05m, 0.10m, 0.15m, 0.20m, 0.30m, 0.40m, 0.50m };
            Random random = new Random();
            var cars = ctx.Cars.ToList();
            var customers = ctx.Customers.ToList();
            var salesExpectedCount = 80;       //random picked number
            for (int i = 0; i < salesExpectedCount; i++)
            {
                Car car = cars[random.Next(cars.Count())];
                Customer customer = customers[random.Next(customers.Count())];
                var discount = discounts[random.Next(discounts.Length)];
                if (customer.IsYoungDriver)
                {
                    discount = discount - 0.05m;
                }
                Sale sale = new Sale()
                {
                    Car = car,
                    Customer = customer,
                    Discount = discount
                };
                ctx.Sales.Add(sale);
            }
            ctx.SaveChanges();
        }
        private static void AddCustomersToDatabase(CarDealerContext ctx)
        {
            XDocument customerXml = XDocument.Load("../../XmlFiles/customers.xml");
            var customers = customerXml.Root.Elements();
            foreach (var c in customers)
            {
                Customer newCustomer = new Customer()
                {
                    Name = c.Attribute("name").Value,
                    BirthDate = DateTime.Parse(c.Element("birth-date").Value),
                    IsYoungDriver = bool.Parse(c.Element("is-young-driver").Value)
                };
                ctx.Customers.Add(newCustomer);
            }
            ctx.SaveChanges();
        }
        private static void AddCarsToDatabase(CarDealerContext ctx)
        {
            XDocument carsXml = XDocument.Load("../../XmlFiles/cars.xml");
            var cars = carsXml.Root.Elements();
            Random rnd = new Random();
            var partsList = ctx.Parts.ToList();
            foreach (var c in cars)
            {
                Car newCar = new Car()
                {
                    Make = c.Element("make").Value,
                    Model = c.Element("model").Value,
                    TravelledDistance = long.Parse(c.Element("travelled-distance").Value)
                };

                int randomParts = rnd.Next(10, 21);
                for (int i = 0; i < randomParts; i++)
                {
                    newCar.Parts.Add(partsList[rnd.Next(partsList.Count())]);
                }
                ctx.Cars.Add(newCar);
            }
            ctx.SaveChanges();
        }
        private static void AddPartsToDatabase(CarDealerContext ctx)
        {
            XDocument partsXml = XDocument.Load("../../XmlFiles/parts.xml");
            var parts = partsXml.Root.Elements();
            Random random = new Random();
            int suppliersCount = ctx.Suppliers.Count();
            var suppliers = ctx.Suppliers.ToList();
            foreach (var p in parts)
            {
                Part newPart = new Part()
                {
                    Name = p.Attribute("name").Value,
                    Price = decimal.Parse(p.Attribute("price").Value),
                    Quantity = int.Parse(p.Attribute("quantity").Value),
                    Supplier = suppliers[random.Next(suppliersCount)]
                };
                ctx.Parts.Add(newPart);
            }
            ctx.SaveChanges();
        }
        private static void AddSuppliersToDatabase(CarDealerContext ctx)
        {
            XDocument suppliersXml = XDocument.Load("../../XmlFiles/suppliers.xml");
            var suppliers = suppliersXml.Root.Elements();
            foreach (var s in suppliers)
            {
                Supplier newSupplier = new Supplier()
                {
                    Name = s.Attribute("name").Value,
                    IsImporter = bool.Parse(s.Attribute("is-importer").Value)
                };
                ctx.Suppliers.Add(newSupplier);
            }
            ctx.SaveChanges();
        }
    }
}
