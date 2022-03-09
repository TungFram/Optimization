using System;

namespace Optimization
{
    public class BrentCombinedFunction
    {
        public double calc(double a, double c, double epsilon)
        {
            double w, v, x;
            x = w = v = (a + c) / 2;
            double fw, fv, fx;
            fx = fw = fv = f(x);
            double d = c - a;
            double e = c - a;
            //double K = (Math.Sqrt(5) - 1) / 2;
            double K = (3 - Math.Sqrt(5)) / 2;
            double u = 0;
            iterAmount = 0;
            amountOfFuncComputation = 0;
            while (Math.Abs(d) > epsilon)
            {
                
                //Console.WriteLine("Interation: " + interamount + ", Current interval: (" + a + ", " + c + ")" + ", xmin: " + ((c + a) / 2) +  ", Current amount of func calculations: " + amountoffunccomputation);
                double g = e;
                e = d;

                if (!(isSame(x, w, v) && isSame(fx, fw, fv)))
                {
                    u = parabolaMin(x, w, v, fx, fw, fv);
                }

                if (a + epsilon <= u && c - epsilon >= u && Math.Abs(u - x) < 0.5 * g)
                {
                    d = Math.Abs(u - x);
                }
                else
                {
                    if (x < (a + c) / 2)
                    {
                        u = x + K * (c - x);
                        d = c - x;
                    }
                    else
                    {
                        u = x - K * (x - a);
                        d = x - a;
                    }
                }
                if (Math.Abs(u - x) < epsilon)
                {
                    u = x + Math.Sign(u - x) * epsilon;
                }
                double fu = f(u);
                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        a = x;
                    }
                    else
                    {
                        c = x;
                    }
                    v = w;
                    w = x;
                    x = u;
                    fv = fw;
                    fw = fx;
                    fx = fu;
                }
                else
                {
                    if (u >= x)
                    {
                        c = u;
                    }
                    else
                    {
                        a = u;
                    }
                    if (fu <= fw || w == x)
                    {
                        v = w;
                        w = u;
                        fv = fw;
                        fw = fu;
                    }
                    else if (fu <= fu || v == x || v == w)
                    {
                        v = u;
                        fv = fu;
                    }
                }
                iterAmount++;
                Console.WriteLine("Interation: " + iterAmount + ", Current interval: (" + a + ", " + c + ")" + ", xmin: " + ((c + a) / 2) +  ", Current amount of func calculations: " + amountOfFuncComputation);
            }
            return x;
        }
        private static double parabolaMin(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return x2 - (Math.Pow(x2 - x1, 2) * (y2 - y3) - Math.Pow(x2 - x3, 2) *
                    (y2 - y1))
                / (2 * ((x2 - x1) * (y2 - y3) - (x2 - x3) * (y2 - y1)));
        }
        bool isSame(double val1, double val2, double val3)
        { 
            return Math.Abs(val1 - val2) < 1e-5 && Math.Abs(val1 - val3) < 1e-5 &&
                 Math.Abs(val2 - val3) < 1e-5;
        }
    }
}