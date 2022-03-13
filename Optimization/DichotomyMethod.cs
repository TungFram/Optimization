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
        
        // [-1,0000005E-06;9,999995E-07], x min:-5,000000000138018E-13
        // [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String")]
        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            IterationCount = 0;
            while (true)
            {
                IterationCount++;
                Console.WriteLine($"[Dichotomy] Iteration: {IterationCount}, current interval: [ {left} ; {right} ]," +
                                  $" x min:{(left + right) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                
                double leftBorder = (left + right) / 2 - exactitude;
                double rightBorder = (left + right) / 2 + exactitude;

                double functionLeftBorder = Function.CalculateFunction(leftBorder);
                double functionRightBorder = Function.CalculateFunction(rightBorder);

                // Если функция от левой границы окрестности больше, отсекаем часть от исходной левой границы до окрестности.
                if (functionLeftBorder > functionRightBorder) 
                {
                    left = leftBorder;
                }
                else
                {
                    // Если функция от правой границы окрестности больше, отсекаем часть от окрестности до исходной правой границы.
                    right = rightBorder;
                }

                // Если дошли до нужной точности, возвращается середина между границами.
                if (Math.Abs(Math.Abs(right) - Math.Abs(left)) <= exactitude)
                {
                    var finalPoint = (left + right) / 2;
                    return new PointAndValue(finalPoint, Function.CalculateFunction(finalPoint));
                }
            }
        }
    }
}