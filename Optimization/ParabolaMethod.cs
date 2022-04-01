using System;
using Optimization;

namespace Optimization
{
    internal class ParabolaMethod
    {
        public readonly Function Function;
        public int IterationCount;

        public ParabolaMethod()
        {
            Function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            // Не обещает сходимости.
            
            double prevMin = 0;
            double middle = right - left;
            double functionMiddle = Function.CalculateFunction(middle);

            if (left == 0)
            {
                left += exactitude;
            }

            if (right == 0)
            {
                right -= exactitude;
            }

            double functionLeft = Function.CalculateFunction(left);
            double functionRight = Function.CalculateFunction(right);

            double curMin = ParabolaVertex(left, functionLeft,
                middle, functionMiddle,
                right, functionRight).Point;
            double functionMin = Function.CalculateFunction(curMin);

            while (Math.Abs(curMin - prevMin) > exactitude)
            {
                IterationCount++;
                Console.WriteLine(
                    $"Iteration: {IterationCount}, Current interval: ({left}:{right})," +
                    $" x min: {curMin}, current amount of func calls: {Function.AmountFunctionCalls}");
                Program.swOutput.WriteLine($"{IterationCount};\"[ {left} ; {right} ]\";{(left + right) / 2};{Function.AmountFunctionCalls}");
                
                prevMin = curMin;

                if (left < curMin && curMin < middle)
                {
                    if (functionMin >= functionMiddle)
                    {
                        left = curMin;
                        functionLeft = functionMin;
                    }
                    else
                    {
                        right = middle;
                        functionRight = functionMiddle;
                        middle = curMin;
                        functionMiddle = functionMin;
                    }
                }
                else if (middle < curMin && curMin < right)
                {
                    if (functionMiddle >= functionMin)
                    {
                        left = middle;
                        functionLeft = functionMiddle;
                        middle = curMin;
                        functionMiddle = functionMin;
                    }
                    else
                    {
                        right = curMin;
                        functionRight = functionMin;
                    }
                }

                var helpMin = ParabolaVertex(left, functionLeft,
                    middle, functionMiddle,
                    right, functionRight).Point;
                if (double.IsNaN(helpMin))
                    continue;

                curMin = helpMin;
                functionMin = Function.CalculateFunction(curMin);
            }
            
            return new PointAndValue(curMin, functionMin);
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