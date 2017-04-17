using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks4To6.Models
{
    public class Car
    {
        public Car()
        {
            this.Parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double TravelledDistance { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
    }
}
