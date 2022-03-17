using System;
using System.Diagnostics.CodeAnalysis;

namespace Optimization
{
    public class DichotomyMethod
    {
        public readonly Function Function;
        public int IterationCount;
        public DichotomyMethod()
        {
            Function = new Function();
        }
        
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String")]
        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            IterationCount = 0;
            
            while (true)
            {
                IterationCount++;
                Console.WriteLine($"[Dichotomy] Iteration: {IterationCount}, current interval: [ {left} ; {right} ]," +
                                  $" x min:{(left + right) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                
                double middle = (left + right) / 2;

                double functionLeftBorder = Function.CalculateFunction(middle - exactitude);
                double functionRightBorder = Function.CalculateFunction(middle + exactitude);

                // Если функция от левой границы окрестности больше, отсекаем часть от исходной левой границы до окрестности.
                if (functionLeftBorder > functionRightBorder)
                {
                    left = middle;
                }
                else
                {
                    // Если функция от правой границы окрестности больше, отсекаем часть от окрестности до исходной правой границы.
                    right = middle;
                }

                // Если дошли до нужной точности, возвращается середина между границами.
                if (Math.Abs(right - left) > exactitude) 
                    continue;
                
                var finalPoint = (left + right) / 2;
                return new PointAndValue(finalPoint, Function.CalculateFunction(finalPoint));
            }
        }
    }
}