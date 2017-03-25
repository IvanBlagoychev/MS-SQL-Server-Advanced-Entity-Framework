using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.Hospital_Datbase
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new HospitalDb();
            //context.Database.Initialize(true);

            Console.Write("Enter patient's first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter patient's last name: ");
            string LastName = Console.ReadLine();
            Console.Write("Enter patient's adress: ");
            string Adress = Console.ReadLine();
            Console.Write("Enter patient's email: ");
            string Email = Console.ReadLine();
            Console.Write("Patient has medical insurance - true/fasle: ");
            bool MedInsurance = bool.Parse(Console.ReadLine());
            DateTime VisitationDate = DateTime.Now;
            Console.Write("Enter patient's diagnose: ");
            string Diagnose = Console.ReadLine();
            Console.Write("Medicaments to prescript: ");
            string Medicaments = Console.ReadLine();


            AddPatient(context, firstName, LastName, Adress, Email, MedInsurance);

            AddDiagnoseMedicamentVisitation(context, firstName, Diagnose, Medicaments);

            
        }

        private static void AddDiagnoseMedicamentVisitation(HospitalDb context, string firstName, string Diagnose, string Medicaments)
        {
            Patient patient = context.Patients.First(e => e.FirstName == firstName);
            Diagnose diagnose = new Diagnose()
            {
                Name = Diagnose
            };
            patient.Diagnoses.Add(diagnose);

            Vsitation visitation = new Vsitation()
            {
                
            };
            patient.Vsitations.Add(visitation);

            Medicament medicaments = new Medicament()
            {
                Name = Medicaments
            };
            patient.Medicaments.Add(medicaments);
            context.SaveChanges();
            Console.WriteLine($"Successfully added patient with name - {firstName} to the database!");
        }

        private static void AddPatient(HospitalDb context, string firstName, string LastName, string Adress, string Email, bool MedInsurance)
        {
            Patient johny = new Patient()
            {
                FirstName = firstName,
                LastName = LastName,
                Adress = Adress,
                Email = Email,
                MedInsurance = MedInsurance
            };
            context.Patients.Add(johny);
            context.SaveChanges();
        }
    }
}
