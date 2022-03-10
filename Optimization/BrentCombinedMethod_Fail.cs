using System;

namespace Optimization
{
    public class BrentCombinedMethod_Fail
    {
        private readonly Function _function;
        public BrentCombinedMethod_Fail()
        {
            _function = new Function();
        }
        // Полагаю, что материал взят отсюда, но это не точно:
        // https://group112.github.io/doc/sem2/2019/2019_sem2_lesson3.pdf
        // Попытался разобрать как мог, чтобы дать НОРМАЛЬНЫЕ названия, но все тщетно.
        public double Min(double a, double b, double eps)
        {
            double w, v, x;
            x = w = v = (a + b) / 2;
            double fw, fv, fx;
            fx = fw = fv = _function.CalculateFunction(x);
            double d = b - a;
            double e = b - a;
            double K = (3 - Math.Sqrt(5)) / 2;
            double u = 0;
            int iterAmount = 1;
            while (Math.Abs(d) > eps)
            {
                iterAmount++;
                Console.WriteLine($"Iteration: {iterAmount}, current interval: [{a};{b}]," +
                                  $" x min:{(b + a) / 2}, amount of function calls: {_function.AmountFunctionCalls}");
                double g = e;
                e = d;

                if (!(IsSame(x, w, v, eps) && IsSame(fx, fw, fv, eps)))
                {
                    u = ParabolaMin(x, w, v, fx, fw, fv);
                }

                if (a + eps <= u && b - eps >= u && Math.Abs(u - x) < 0.5 * g)
                {
                    d = Math.Abs(u - x);
                }
                else
                {
                    if (x < (a + b) / 2)
                    {
                        u = x + K * (b - x);
                        d = b - x;
                    }
                    else
                    {
                        u = x - K * (x - a);
                        d = x - a;
                    }
                }
                if (Math.Abs(u - x) < eps)
                {
                    u = x + Math.Sign(u - x) * eps;
                }
                double fu = _function.CalculateFunction(u);
                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        a = x;
                    }
                    else
                    {
                        b = x;
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
                        b = u;
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
            }
            return x;
        }
        private static double ParabolaMin(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return x2 - (Math.Pow(x2 - x1, 2) * (y2 - y3) - Math.Pow(x2 - x3, 2) *
                    (y2 - y1))
                / (2 * ((x2 - x1) * (y2 - y3) - (x2 - x3) * (y2 - y1)));
        }

        private static bool IsSame(double first, double second, double third, double exactitude = 0.001)
        { 
            return Math.Abs(first - second) < exactitude &&
                   Math.Abs(first - third)  < exactitude &&
                   Math.Abs(second - third) < exactitude;
        }
    }
}