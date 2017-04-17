using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks4To6.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsImporter { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
    }
}
