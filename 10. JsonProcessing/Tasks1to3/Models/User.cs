using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1to3.Models
{
    public class User
    {
        public User()
        {
            this.ProductsBought = new HashSet<Product>();
            this.ProductsSold = new HashSet<Product>();
            this.Friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
                
        [Required, MinLength(3)]
        public string LastName { get; set; }
        public int? Age { get; set; }

        //[InverseProperty("Seller")]
        public ICollection<Product> ProductsSold { get; set; }

        //[InverseProperty("Buyer")]
        public ICollection<Product> ProductsBought { get; set; }
        public virtual ICollection<User> Friends { get; set; }
    }
}
