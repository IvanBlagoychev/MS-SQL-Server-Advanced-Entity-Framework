using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks5to10.Models
{
    public class Photographer
    {
        public Photographer()
        {
            this.Albums = new HashSet<Album>();
            this.ViewerAlbums = new HashSet<Album>();
        }
        public Photographer(string username, string email, DateTime register, DateTime birthdate)
        {
            this.Username = username;
            this.Email = email;
            this.RegisterDate = register;
            this.BirthDate = birthdate;
            this.Albums = new HashSet<Album>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        [InverseProperty("Viewers")]
        public virtual ICollection<Album> ViewerAlbums { get; set; }
    }
}
