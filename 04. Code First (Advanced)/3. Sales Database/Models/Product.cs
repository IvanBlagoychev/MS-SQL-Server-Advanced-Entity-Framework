namespace _3.Sales_Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        //Part of task 4.Product Migration\\
        public string Description { get; set; }
        public List<Sale> SalesOfProduct { get; set; }
    }
}