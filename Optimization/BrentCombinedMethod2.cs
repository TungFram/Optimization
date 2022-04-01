using System;

namespace Optimization
{
    public class BrentCombinedMethod2
    {
        private readonly double _proportion = (3 - Math.Sqrt(5)) / 2; // K
        private double _exactitude = 0; // tol
        public readonly Function Function;
        public int IterationCount;
        public BrentCombinedMethod2()
        {
            Function = new Function();
        }
        // Mатериал взят отсюда:
        // http://www.machinelearning.ru/wiki/images/4/4d/MOMO16_min1d.pdf

        public PointAndValue Min(double left /* a */, double right/* c */, double exactitude = 0.001 /* ε */)
        {
            double actualLenght = right - left; // d
            double previousLenght = actualLenght; // e
            
            double minOfParabola = 0; // u
            double functionMinOfParabola = 0; // fu
            
            double secondFromBelow, prevSecondFromBelow; // w v
            double functionSecondFromBelow, functionPrevSecondFromBelow; // fw fv
            
            var min = secondFromBelow = prevSecondFromBelow =
                left + _proportion * actualLenght; // x w v
            var functionMin = functionSecondFromBelow =
                functionPrevSecondFromBelow = Function.CalculateFunction(min); // fx fw fv
            
            IterationCount = 0;
            while (Math.Abs(actualLenght) > exactitude)
            {
                IterationCount++;
                Console.WriteLine($"[Brent2] Iteration: {IterationCount}, current interval: [{left};{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                double g = previousLenght; // ?
                previousLenght = actualLenght;
                _exactitude = exactitude * Math.Abs(min) + exactitude / 10; // ?

                // Kритерий останова.
                if ((Math.Abs(min - (left + right) / 2) + (right - left) / 2) <= (2 * _exactitude))
                    return new PointAndValue(min, Function.CalculateFunction(min));

                if (!Equals(min, secondFromBelow, prevSecondFromBelow, exactitude) &&
                    !Equals(functionMin, functionSecondFromBelow, functionPrevSecondFromBelow, exactitude))
                {
                    minOfParabola = ParabolaMin(min, secondFromBelow, prevSecondFromBelow,
                        functionMin, functionSecondFromBelow, functionPrevSecondFromBelow);

                    if (minOfParabola >= left && minOfParabola <= right &&
                        Math.Abs(minOfParabola - min) < g / 2)
                    {
                        // Принимаем u ???

                        if (minOfParabola - left  < 2 * _exactitude ||
                            right - minOfParabola < 2 * _exactitude)
                        {
                            minOfParabola = min - Math.Sign(min - (left + right) / 2) * _exactitude;
                        }
                    }
                }
                else
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
                
                if (Math.Abs(minOfParabola - min) < _exactitude)
                {
                    minOfParabola = min + Math.Sign(minOfParabola - min) * _exactitude;  // Задаём минимальную близость между minOfParabola и min
                }

                actualLenght = Math.Abs(minOfParabola - min); // d = |u − x|;
                functionMinOfParabola = Function.CalculateFunction(minOfParabola); // fu
                
                if (functionMinOfParabola <= functionMin)
                {
                    if (minOfParabola >= min)
                    {
                        left = min;
                    }
                    else
                    {
                        right = min;
                    }
                    
                    prevSecondFromBelow = secondFromBelow;
                    secondFromBelow = min;
                    min = minOfParabola;
                    functionPrevSecondFromBelow = functionSecondFromBelow;
                    functionSecondFromBelow = functionMin;
                    functionMin = functionMinOfParabola;
                }
                else
                {
                    if (minOfParabola >= min)
                    {
                        right = minOfParabola;
                    }
                    else
                    {
                        left = minOfParabola;
                    }
                    if (functionMinOfParabola <= functionSecondFromBelow ||
                        Math.Abs(secondFromBelow - min) < exactitude)
                    {
                        prevSecondFromBelow = secondFromBelow;
                        secondFromBelow = minOfParabola;
                        functionPrevSecondFromBelow = functionSecondFromBelow;
                        functionSecondFromBelow = functionMinOfParabola;
                    }
                    else if (functionMinOfParabola <= functionPrevSecondFromBelow ||
                             Math.Abs(prevSecondFromBelow - min) < exactitude ||
                             Math.Abs(prevSecondFromBelow - secondFromBelow) < exactitude)
                    {
                        prevSecondFromBelow = minOfParabola;
                        functionPrevSecondFromBelow = functionMinOfParabola;
                    }
                }
            }
            return new PointAndValue(min, Function.CalculateFunction(min));
        }
        
        private static double ParabolaMin(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return x2 - (Math.Pow(x2 - x1, 2) * (y2 - y3) - Math.Pow(x2 - x3, 2) * (y2 - y1))
                        /
                        (2 * ((x2 - x1) * (y2 - y3) - (x2 - x3) * (y2 - y1)));
        }

        private static bool Equals(double first, double second, double third, double exactitude = 0.001)
        { 
            return Math.Abs(first - second) < exactitude &&
                   Math.Abs(first - third)  < exactitude &&
                   Math.Abs(second - third) < exactitude;
        }
    }
}