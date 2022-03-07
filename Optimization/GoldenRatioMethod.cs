using System;

namespace Optimization
{
    public class GoldenRatioMethod : Method
    {
        private static double it_num;
        private static double funccall_num;
        public double calc(double epsilon, double a, double b)
        {
            double GRconst = (3 - Math.Sqrt(5)) / 2;
            double interval = b - a;
            double shear_length = GRconst * interval;
            double x1 = a + shear_length;
            double x2 = b - shear_length;
            double f_x1 = f(x1);
            double f_x2 = f(x2);
            double previouslen = b - a;
            funccall_num = 0;
            it_num = 0;

            while (interval > epsilon)
            {
                it_num += 1;
                Console.WriteLine("Номер итерации: " + it_num + ", current interval: " + a + ", " + b + ", difference bewteen stuff: " + (previouslen / Math.Abs(b-a)) + ", Amount of func calls: " + funccall_num);
                previouslen = b - a;
                shear_length = GRconst * interval;
                
                if (f_x1 > f_x2)
                {
                    a = x1;
                    interval = b - a;
                    x1 = x2;
                    x2 = a + shear_length;
                    f_x1 = f_x2;
                    f_x2 = f(x2);
                }
                else
                {
                    b = x2;
                    interval = b - a;
                    x2 = x1;
                    x1 = b - shear_length;
                    f_x2 = f_x1;
                    f_x1 = f(x1);
                }
            }

            if (f(a) < f(b))
            {
                Console.WriteLine("Minimum: " + a);
                return a;
            }
            else
            {
                Console.WriteLine("Minimum: " + b);
                return b;
            }

            double f(double x)
            {
                funccall_num += 1;
                return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
            }
        }
    }
}