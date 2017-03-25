using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.Hospital_Datbase
{
    public class Patient
    {
        public Patient()
        {
            this.Diagnoses = new List<Diagnose>();
            this.Medicaments = new List<Medicament>();
            this.Vsitations = new List<Vsitation>();
        }

        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Adress { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime? DoB { get; set; }
        
        public byte[] Picture { get; set; }

        public bool MedInsurance { get; set; }

              

        public virtual List<Medicament> Medicaments { get; set; }
        public virtual List<Vsitation> Vsitations { get; set; }
        public virtual List<Diagnose> Diagnoses { get; set; }

    }
}
