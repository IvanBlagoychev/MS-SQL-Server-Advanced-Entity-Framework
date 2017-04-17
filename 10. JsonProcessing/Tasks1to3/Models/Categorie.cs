using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tasks1to3.Models
{
    public class Categorie
    {
        public Categorie()
        {
            this.Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required,MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
