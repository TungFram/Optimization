using System;

namespace Optimization
{
    public class DichotomyMethod:Method //TODO:Почему на одну итерацию больше чем нужно
    {
        //public static int interamount;
        public static double deltaa;
        public static double xmin;
        private static double previouslength;

        public DichotomyMethod()
        {
            interamount = 0;
            amountoffunccomputation = 0;
        }
        public double calc(double epsilon, double a, double b)
        {
            xmin = 0;
            while (Math.Abs(b-a) > epsilon)
            {
                interamount++;
                double x1 = ((a + b) / 2) - epsilon/4;
                double x2 = ((a + b) / 2) + epsilon/4;
                double fx1 = f(x1);
                double fx2 = f(x2);
                previouslength = b - a;
                if (fx1 > fx2)
                {
                    a = x1;
                }
                else if (fx1 < fx2)
                {
                    b = x2;
                }
                else
                {
                    a = x1;
                    b = x2;
                }
                Console.WriteLine("Interation: " + interamount + ", Current interval: (" + a + ", " + b + ")" + ", difference betweent previous interval: " + (previouslength / Math.Abs(b - a)) + ", xmin: " + ((b + a) / 2) +  ", Current amount of func calculations: " + amountoffunccomputation);
            }
            xmin = (a + b) / 2;
            return xmin;
        }
    }
}