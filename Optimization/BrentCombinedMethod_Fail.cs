using System;

namespace Optimization
{
    public class BrentCombinedMethod_Fail
    {
        private readonly double _proportion = (3 - Math.Sqrt(5)) / 2;
        private readonly Function _function;
        public BrentCombinedMethod_Fail()
        {
            _function = new Function();
        }
        // Полагаю, что материал взят отсюда, но это не точно:
        // https://group112.github.io/doc/sem2/2019/2019_sem2_lesson3.pdf
        // Или отсюда:
        // http://www.machinelearning.ru/wiki/images/4/4d/MOMO16_min1d.pdf
        // Попытался разобрать как мог, чтобы дать НОРМАЛЬНЫЕ названия, но все тщетно.
        public double Min(double left, double right, double exactitude = 0.001)
        {
            double w, v;
            double fw, fv;
            var x = w = v = (left + right) / 2;
            var fx = fw = fv = _function.CalculateFunction(x);
            double actualStep = right - left;
            double prevStep = actualStep;
            double minOfParabola = 0;
            int iterAmount = 1;
            while (Math.Abs(actualStep) > exactitude)
            {
                iterAmount++;
                Console.WriteLine($"Iteration: {iterAmount}, current interval: [{left};{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {_function.AmountFunctionCalls}");
                double g = prevStep;
                prevStep = actualStep;

                if (!(IsSame(x, w, v, exactitude) && IsSame(fx, fw, fv, exactitude)))
                {
                    minOfParabola = ParabolaMin(x, w, v, fx, fw, fv);
                }

                if (left + exactitude <= minOfParabola && right - exactitude >= minOfParabola && Math.Abs(minOfParabola - x) < 0.5 * g)
                {
                    actualStep = Math.Abs(minOfParabola - x);
                }
                else
                {
                    if (x < (left + right) / 2)
                    {
                        minOfParabola = x + _proportion * (right - x);
                        actualStep = right - x;
                    }
                    else
                    {
                        minOfParabola = x - _proportion * (x - left);
                        actualStep = x - left;
                    }
                }
                if (Math.Abs(minOfParabola - x) < exactitude)
                {
                    minOfParabola = x + Math.Sign(minOfParabola - x) * exactitude;
                }
                double fu = _function.CalculateFunction(minOfParabola);
                if (fu <= fx)
                {
                    if (minOfParabola >= x)
                    {
                        left = x;
                    }
                    else
                    {
                        right = x;
                    }
                    v = w;
                    w = x;
                    x = minOfParabola;
                    fv = fw;
                    fw = fx;
                    fx = fu;
                }
                else
                {
                    if (minOfParabola >= x)
                    {
                        right = minOfParabola;
                    }
                    else
                    {
                        left = minOfParabola;
                    }
                    if (fu <= fw || w == x)
                    {
                        v = w;
                        w = minOfParabola;
                        fv = fw;
                        fw = fu;
                    }
                    else if (fu <= fu || v == x || v == w)
                    {
                        v = minOfParabola;
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