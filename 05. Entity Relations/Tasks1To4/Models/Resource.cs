using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1To4.Models
{
    public class Resource
    {
        public enum ResType
        {
            video,
            presentation,
            document,
            other
        }
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }
        
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ResType ResourceType { get; set; }
        [Required]
        public string URL { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}
