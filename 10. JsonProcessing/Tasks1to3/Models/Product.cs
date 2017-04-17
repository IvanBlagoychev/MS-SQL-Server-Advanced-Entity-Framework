using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks1to3.Models
{
    public class Product
    {
        public Product()
        {
            this.Categories = new HashSet<Categorie>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }
        public decimal? Price { get; set; }

        //[ForeignKey("Buyer")]
        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        //[ForeignKey("Seller")]
        public int SellerId { get; set; }

        public User Seller { get; set; }
        
        public ICollection<Categorie> Categories { get; set; }
    }
}

