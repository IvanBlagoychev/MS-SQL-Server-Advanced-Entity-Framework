using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _09.Hospital_Datbase
{
    public class Medicament
    {
        public Medicament()
        {
            this.Patients = new List<Patient>();
        }
        [Key]
        public int MedicamentId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual List<Patient> Patients { get; set; }
    }
}
