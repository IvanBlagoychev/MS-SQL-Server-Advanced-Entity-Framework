﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First___OOP_Intro_Exercise
{
    class Calculation
    {
        public static  double planck = 6.62606896e-34;
        public static double pi = 3.14159;
        public static double GetResult()
        {
            return planck / (2 * pi);
        }
        
    }
}
