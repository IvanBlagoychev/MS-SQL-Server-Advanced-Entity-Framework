using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks5to10.Models
{
    public class Album
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Viewers = new HashSet<Photographer>();
            this.Photographers = new HashSet<Photographer>();
        }
        public Album(string name, string color, bool isPublic)
        {
            this.Name = name;
            this.BackgroundColor = color;
            this.IsPublic = isPublic;
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Viewers = new HashSet<Photographer>();
            this.Photographers = new HashSet<Photographer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public bool IsPublic { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        //public virtual Photographer Owner { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        [InverseProperty("ViewerAlbums")]
        public virtual ICollection<Photographer> Viewers { get; set; }
        public virtual ICollection<Photographer> Photographers { get; set; }
    }
}
