using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks5to10.Models
{
    public class Tag
    {
        public Tag()
        {

        }
        public Tag(string content)
        {
            this.Content = content;
            this.Albums = new HashSet<Album>();
        }
        public int Id { get; set; }
        [Tag]
        public string Content { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
