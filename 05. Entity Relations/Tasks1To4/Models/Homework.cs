using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1To4.Models
{
    public class Homework
    {
        public enum HomeType
        {
            application,
            pdf,
            zip
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public HomeType ContentType { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        
        
    }
}
