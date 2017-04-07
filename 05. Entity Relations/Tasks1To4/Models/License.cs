using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1To4.Models
{
    public class License
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
