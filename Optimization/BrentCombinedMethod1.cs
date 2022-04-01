using System;

namespace Optimization
{
    public class BrentCombinedMethod1
    {
        // Алгоритм взят отсюда:
        // https://group112.github.io/doc/sem2/2019/2019_sem2_lesson3.pdf
        public readonly Function Function;
        public int IterationCount;
        private readonly double _proportion = (3 - Math.Sqrt(5)) / 2; // K

        public BrentCombinedMethod1()
        {
            Function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            IterationCount = 0;
            double actualLenght = right - left; // d
            double previousLenght = actualLenght; // e

            double secondFromBelow, prevSecondFromBelow; // w v

            var min = secondFromBelow = prevSecondFromBelow =
                left + _proportion * actualLenght; // x w v
            var functionMin = Function.CalculateFunction(min); // fx fw fv

            while (true)
            {
                IterationCount++;
                Console.WriteLine($"[Brent1] Iteration: {IterationCount}, current interval: [{left};{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                
                if (Math.Max(
                    Math.Abs(min - left),
                    Math.Abs(right - min)) < exactitude)
                {
                    return new PointAndValue(min, Function.CalculateFunction(min));
                }

                double heuristics = previousLenght / 2;
                previousLenght = actualLenght;
                double functionSecondFromBelow = Function.CalculateFunction(secondFromBelow); // fw
                double functionPrevSecondFromBelow = Function.CalculateFunction(prevSecondFromBelow); // fv
                
                double minOfParabola = ParabolaVertex(min, functionMin, // x fx
                        secondFromBelow, functionSecondFromBelow, // w fw
                        prevSecondFromBelow, functionPrevSecondFromBelow). // v fv
                    Point; // u

                if (double.IsNaN(minOfParabola) ||
                    minOfParabola < left || minOfParabola > right ||
                    Math.Abs(Math.Abs(minOfParabola) - Math.Abs(min)) > heuristics)
                {
                    if (min < (left + right) / 2)
                    {
                        minOfParabola = min + _proportion * (right - min); // Золотое сечение [min, right]
                        previousLenght = right - min;
                    }
                    else
                    {
                        minOfParabola = min - _proportion * (min - left); // Золотое сечение [left, min]
                        previousLenght = min - left;
                    }
                }

                actualLenght = Math.Abs(Math.Abs(minOfParabola) - Math.Abs(min));
                double functionMinOfParabola = Function.CalculateFunction(minOfParabola); // fu

                if (functionMinOfParabola > functionMin)
                {
                    if (minOfParabola < min)
                    {
                        left = minOfParabola;
                    }
                    else
                    {
                        right = minOfParabola;
                    }

                    if (functionMinOfParabola <= functionSecondFromBelow ||
                        Math.Abs(min - secondFromBelow) < exactitude)
                    {
                        prevSecondFromBelow = secondFromBelow;
                        secondFromBelow = minOfParabola;
                    }
                    else if (functionMinOfParabola <= functionPrevSecondFromBelow ||
                             Math.Abs(prevSecondFromBelow - min) < exactitude ||
                             Math.Abs(prevSecondFromBelow - secondFromBelow) < exactitude)
                    {
                        prevSecondFromBelow = minOfParabola;
                    }
                }
                else
                {
                    if (minOfParabola < min)
                    {
                        right = min;
                    }
                    else
                    {
                        left = min;
                    }

                    prevSecondFromBelow = secondFromBelow;
                    secondFromBelow = min;
                    min = minOfParabola;
                }
            }
        }

        private static PointAndValue ParabolaVertex(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            if (Equals(x1, x2, x3) || Equals(y1, y2, y3))
                return new PointAndValue(double.NaN, double.NaN);
            
            double denominator = (x1 - x2) * (x1 - x3) * (x2 - x3);
            double a     = (x3 * (y2 - y1)  +  x2 * (y1 - y3)  +  x1 * (y3 - y2)) / denominator;
            double b     = (x3*x3 * (y1 - y2) + x2*x2 * (y3 - y1) + x1*x1 * (y2 - y3)) / denominator;
            double c     = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denominator;

            return new PointAndValue(-b / (2 * a), c - b * b / (4 * a));
        }
        
        private static bool Equals(double first, double second, double third, double exactitude = 0.000001)
        { 
            return Math.Abs(first - second) < exactitude &&
                   Math.Abs(first - third)  < exactitude &&
                   Math.Abs(second - third) < exactitude;
        }
    }
}