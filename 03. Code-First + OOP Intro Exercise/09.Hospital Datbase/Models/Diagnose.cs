using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.Hospital_Datbase
{
    public class Diagnose
    {
        public Diagnose()
        {
            this.Patients = new List<Patient>();
        }
        [Key]
        public int DiagnoseId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        public virtual List<Patient> Patients { get; set; }
    }
}
