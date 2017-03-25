namespace Code_First___OOP_Intro_Exercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {
            //DefineClassPerson();
            //CreatePersonContructors();
            //OldestFamilyMember();
            //Students();
            //PlanckConstant();
            //MathUtilities();

        }

        private static void MathUtilities()
        {
            var input = Console.ReadLine().Split(' ');
            List<double> result = new List<double>();
            double num1 = double.Parse(input[1]);
            double num2 = double.Parse(input[2]);
            while (input[0] != "End")
            {
                input = Console.ReadLine().Split(' ');
                if (input.Count() > 1)
                {
                    num1 = double.Parse(input[1]);
                    num2 = double.Parse(input[2]);
                    switch (input[0])
                    {
                        case "Sum": result.Add(MathUtil.Sum(num1, num2)); break;
                        case "Subtract": result.Add(MathUtil.Subtract(num1, num2)); break;
                        case "Multiply": result.Add(MathUtil.Multiply(num1, num2)); break;
                        case "Divide": result.Add(MathUtil.Divide(num1, num2)); break;
                        case "Percentage": result.Add(MathUtil.Percentage(num1, num2)); break;
                        default: break;
                    }
                }
                else
                {
                    break;
                }
            }
            foreach (var num in result)
            {
                Console.WriteLine($"{num:f2}");
            }
        }

        private static void PlanckConstant()
        {
            //---------------- TASK 5-----------------\\
            Console.WriteLine(Calculation.GetResult());
        }

        private static void Students()
        {
            // ----------------- TASK 4-------------------\\
            string input = Console.ReadLine();
            while (input != "End")
            {
                input = Console.ReadLine();
                Student st = new Student(input);
            }
            Console.WriteLine(Student.Count);
        }

        private static void OldestFamilyMember()
        {
            //-------------------------------TASK 3----------------------------------\\
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();
            for (int i = 0; i < n; i++)
            {
                var data = Console.ReadLine().Split(' ');
                string name = data[0];
                int age = int.Parse(data[1]);
                family.AddMember(new Person(name, age));
            }
            Console.WriteLine($"Oldest Person: {family.GetOldestPerson().Name} {family.GetOldestPerson().Age}");
        }

        private static void CreatePersonContructors()
        {
            //----------------------TASK 2--------------------------\\

            string[] data = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length == 0)
            {
                Person p = new Person();
                Console.WriteLine(p.Name + " " + p.Age);
            }
            else if (data.Length == 1)
            {
                string argument = data[0];
                int age = -1;
                if (int.TryParse(argument, out age))
                {
                    Person p = new Person(age);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
                else
                {
                    Person p = new Person(argument);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
            }
            else if (data.Length == 2)
            {
                Person p = new Person(data[0], int.Parse(data[1]));
                Console.WriteLine($"{p.Name} {p.Age}");
            }
        }

        private static void DefineClassPerson()
        {
            //-----------------TASK 1--------------------------\\

            Person person1 = new Person()
            {
                Name = "Pesho",
                Age = 20
            };
            Person person2 = new Person()
            {
                Name = "Gosho",
                Age = 18
            };
            Person person3 = new Person()
            {
                Name = "Stamat",
                Age = 43
            };

            Console.WriteLine("Name  Age");
            Console.WriteLine($"{person1.Name}  {person1.Age}");
            Console.WriteLine(person2.Name + " " + person2.Age);
            Console.WriteLine(person3.Name + " " + person3.Age);
        }
    }
}
