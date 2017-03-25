using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _1.Local_Store
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string name { get; set; }

        [StringLength(30)]
        public string distributorName { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public decimal price { get; set; }
    }
}
