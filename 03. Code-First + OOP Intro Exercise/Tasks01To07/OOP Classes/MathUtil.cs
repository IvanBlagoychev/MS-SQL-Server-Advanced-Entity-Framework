

namespace Code_First___OOP_Intro_Exercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class MathUtil
    {       
        
        public static double Sum(double firstNum, double secondNum)
        {
            double sum = firstNum + secondNum;
            return sum;
        }
        public static double Subtract(double firstNum, double secondNum)
        {
            double sum = firstNum - secondNum;
            return sum;
        }
        public static double Multiply(double firstNum, double secondNum)
        {
            double sum = firstNum * secondNum;
            return sum;
        }
        public static double Divide(double firstNum, double secondNum)
        {
            double sum = firstNum / secondNum;
            return sum;
        }
        public static double Percentage(double totalNum, double percent)
        {
            double sum = (totalNum * percent) / 100;
            return sum;
        }
    }
}
