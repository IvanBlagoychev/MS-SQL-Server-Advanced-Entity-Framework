using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First___OOP_Intro_Exercise
{
    class Person
    {
        public string Name;
        public int Age;

        //public Person()
        //{
        //    this.Name = "No name";
        //    this.Age = 1;
        //}

        //public Person(int age)
        //{
        //    this.Name = "No name";
        //    this.Age = age;
        //}

        //public Person(string name)
        //{
        //    this.Name = name;
        //    this.Age = 1;
        //}
        //public Person(string name, int age)
        //{
        //    this.Name = name;
        //    this.Age = age;
        //}


        public Person() : this ("No name", 1) {}

        public Person(int age) : this ("No name", age)
        {
        }

        public Person(string name) :this (name, 1)
        {  
        }
        public Person(string name, int age) 
        {
            this.Name = name;
            this.Age = age;       
        }
    }
}
