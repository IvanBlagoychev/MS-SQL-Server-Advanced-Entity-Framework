using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First___OOP_Intro_Exercise
{
    class Student
    {
        private string StudentName;
        public static int Count;
        public Student(string name)
        {
            this.StudentName = name;
            Count++;
        }
        public string Name
        {
            get
            {
                return this.StudentName;
            } 
            set
            {
                this.StudentName = value;
            }
         }
    }
}
