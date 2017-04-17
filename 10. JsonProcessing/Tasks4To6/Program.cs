namespace Tasks4To6
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Tasks4To6.Data;
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CarDealerContext();
            //ctx.Database.Initialize(true);

            //=============== Task 5 ================\\
            //AddSuplliersToDatabase(ctx);
            //AddPartsToDatabase(ctx);
            //AddCarsWithPartsToDatabase(ctx);
            //AddCustomersToDatabase(ctx);
            //AddSalesTosDatabase(ctx);



            //================ Task 6 ================\\
            //Query1OrderedCustomers(ctx);
            //Query2CarsFromMakeToyota(ctx);
            //Query3LocalSuppliers(ctx);
            //Query4CarsWithTheirListOfParts(ctx);
            //Query5TotalSalesByCustomer(ctx);
            //Query6SalesWithAppliedDiscount(ctx);
        }

        private static void Query5TotalSalesByCustomer(CarDealerContext ctx)
        {
            var totalSales = ctx.Customers
                            .Where(c => c.Sales.Count >= 1)
                            .Select(p => new
                            {
                                fullName = p.Name,
                                boughtCars = p.Sales.Count(),
                                spentMoney = p.Sales.Sum(s => s.Car.Parts.Sum(q => q.Price))
                            }).ToList().OrderByDescending(s => s.spentMoney).ThenByDescending(b => b.boughtCars);
            string totalSalesJson = JsonConvert.SerializeObject(totalSales, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/TotalSalesByCustomer.json", totalSalesJson);
            Console.WriteLine(totalSalesJson);
        }

        private static void Query6SalesWithAppliedDiscount(CarDealerContext ctx)
        {
            var sales = ctx.Sales.Select(c => new
            {
                car = new
                {
                    Make = c.Car.Make,
                    Model = c.Car.Model,
                    TravelledDistance = c.Car.TravelledDistance
                },
                customerName = c.Customer.Name,
                Discount = c.Discount,
                price = c.Car.Parts.Sum(p => p.Price),
                priceWithDiscount = c.Car.Parts.Sum(p => p.Price) - (c.Car.Parts.Sum(p => p.Price) * c.Discount)
            });
            string salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/SalesWithAppliedDiscount.json", salesJson);
            Console.WriteLine(salesJson);
        }

        private static void Query4CarsWithTheirListOfParts(CarDealerContext ctx)
        {
            var cars = ctx.Cars.Select(c => new
            {
                car = new
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravvelledDistance = c.TravelledDistance
                },
                parts = c.Parts.Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price
                })
            });

            string carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/CarsWithTheirListOfParts.json", carsJson);
            Console.WriteLine(carsJson);
        }

        private static void Query3LocalSuppliers(CarDealerContext ctx)
        {
            var suppliers = ctx.Suppliers
                            .Where(i => i.IsImporter == false)
                            .Select(d => new
                            {
                                Id = d.Id,
                                Name = d.Name,
                                PartsCount = d.Parts.Count()
                            });
            string suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/LocalSuppliers.json", suppliersJson);
            Console.WriteLine(suppliersJson);
        }

        private static void Query2CarsFromMakeToyota(CarDealerContext ctx)
        {
            var cars = ctx.Cars
                            .Where(m => m.Make == "Toyota")
                            .OrderBy(m => m.Model)
                            .ThenBy(t => t.TravelledDistance)
                            .Select(i => new
                            {
                                Id = i.Id,
                                Make = i.Make,
                                Model = i.Model,
                                TravelledDistance = i.TravelledDistance
                            });
            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/CarsFromMakeToyota.json", carsJson);
            Console.WriteLine(carsJson);
        }

        private static void Query1OrderedCustomers(CarDealerContext ctx)
        {
            var customers = ctx.Customers
                            .OrderBy(b => b.BirthDate)
                            .ThenBy(i => i.IsYoungDriver)
                            .Select(n => new
                            {
                                Id = n.Id,
                                Name = n.Name,
                                BirtDate = n.BirthDate,
                                IsYoungDriver = n.IsYoungDriver,
                                Sales = n.Sales.Select(k => new
                                {
                                    k.Id,
                                    k.CarId,
                                    k.CustomerId,
                                    k.Discount
                                })
                            });

            string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText("../../NewJsonFiles/OrderedCustomers.json", customersJson);
            Console.WriteLine(customersJson);
        }

        private static void AddSalesTosDatabase(CarDealerContext ctx)
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
            var customers = File.ReadAllText("../../JsonFiles/customers.json");
            List<Customer> customersJson = JsonConvert.DeserializeObject<List<Customer>>(customers);
            ctx.Customers.AddRange(customersJson);
            ctx.SaveChanges();
        }

        private static void AddCarsWithPartsToDatabase(CarDealerContext ctx)
        {
            var cars = File.ReadAllText("../../JsonFiles/cars.json");
            List<Car> carsjson = JsonConvert.DeserializeObject<List<Car>>(cars);
            Random random1 = new Random();
            var partsList = ctx.Parts.ToList();
            foreach (Car c in carsjson)
            {
                int randomParts = random1.Next(10, 21);
                for (int i = 0; i < randomParts; i++)
                {
                    c.Parts.Add(partsList[random1.Next(partsList.Count())]);
                }
            }
            ctx.Cars.AddRange(carsjson);
            ctx.SaveChanges();
        }

        private static void AddPartsToDatabase(CarDealerContext ctx)
        {
            var parts = File.ReadAllText("../../JsonFiles/parts.json");
            List<Part> partsJson = JsonConvert.DeserializeObject<List<Part>>(parts);
            Random random = new Random();
            int suppliersCount = ctx.Suppliers.Count();
            var suppliers = ctx.Suppliers.ToList();
            foreach (Part p in partsJson)
            {
                p.Supplier = suppliers[random.Next(suppliersCount)];
            }
            ctx.Parts.AddRange(partsJson);
            ctx.SaveChanges();
        }

        private static void AddSuplliersToDatabase(CarDealerContext ctx)
        {
            var suplliers = File.ReadAllText("../../JsonFiles/suppliers.json");
            List<Supplier> suppliersJson = JsonConvert.DeserializeObject<List<Supplier>>(suplliers);
            ctx.Suppliers.AddRange(suppliersJson);
            ctx.SaveChanges();
        }
    }
}
