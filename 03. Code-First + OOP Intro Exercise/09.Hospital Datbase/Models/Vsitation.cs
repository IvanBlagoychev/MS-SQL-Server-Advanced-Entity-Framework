using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.Hospital_Datbase
{
    public class Vsitation
    {
        public Vsitation()
        {
            this.Patients = new List<Patient>();
        }

        [Key]
        public int VisitationId { get; set; }

        public DateTime? VisitDate { get; set; }


        [StringLength(100)]
        public string Comment { get; set; }

        public virtual List<Patient> Patients { get; set; }
    }
}
