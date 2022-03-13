using System;
using System.Diagnostics.CodeAnalysis;

namespace Optimization
{
    public class DichotomyMethod
    {
        public readonly Function Function;
        public int IterationCount;
        private static double _left = 0;
        private static double _right = 0;
        public DichotomyMethod()
        {
            Function = new Function();
        }
        
        // [-1,0000005E-06;9,999995E-07], x min:-5,000000000138018E-13
        // [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String")]
        public PointAndValue Min(double start, double end, double exactitude = 0.001)
        {
            _left = start; 
            _right = end;
            IterationCount = 0;
            while (true)
            {
                IterationCount++;
                Console.WriteLine($"[Dichotomy] Iteration: {IterationCount}, current interval: [{_left};{_right}]," +
                                  $" x min:{(_left + _right) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                
                double leftBorder = (_left + _right) / 2 - exactitude;
                double rightBorder = (_left + _right) / 2 + exactitude;

                double functionLeftBorder = Function.CalculateFunction(leftBorder);
                double functionRightBorder = Function.CalculateFunction(rightBorder);
                
                // Если дошли до нужной точности, возвращается середина между границами.
                if (Math.Abs(_right - _left) <= exactitude)
                {
                    var finalPoint = (_left + _right) / 2;
                    return new PointAndValue(finalPoint, Function.CalculateFunction(finalPoint));
                }

                // Если функция от левой границы окрестности больше, отсекаем часть от исходной левой границы до окрестности.
                if (functionLeftBorder > functionRightBorder) 
                {
                    _left = leftBorder;
                    continue;
                }

                // Если функция от правой границы окрестности больше, отсекаем часть от окрестности до исходной правой границы.
                _right = rightBorder;
            }
        }
    }
}