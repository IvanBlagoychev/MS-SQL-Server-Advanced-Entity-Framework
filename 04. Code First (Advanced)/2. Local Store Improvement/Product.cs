using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2.Local_Store_Improvement
{
    public class Product
    {
        public Product()
        {

        }
        public Product(string Name, string Distibutor, string Description, decimal Price, int Quantity, double Weight)
        {
            this.name = Name;
            this.distributorName = Distibutor;
            this.description = Description;
            this.price = Price;
            this.quantity = Quantity;
            this.weight = Weight;
        }
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string name { get; set; }

        [StringLength(30)]
        public string distributorName { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public double weight { get; set; }
    }
}
