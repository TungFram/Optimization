using System;

namespace Optimization
{
    public class GoldenRatioMethod
    {
        // пропорция золотого сечения
        private readonly double _proportion = (1 + Math.Sqrt(5)) / 2;
        private readonly Function _function;
        private double _iterCount;

        public GoldenRatioMethod()
        {
            _function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            double interval = Math.Abs(right - left);
            double leftBorder = right - interval / _proportion;
            double rightBorder = left + interval / _proportion;

            double functionLeftBorder = _function.CalculateFunction(leftBorder);
            double functionRightBorder = _function.CalculateFunction(rightBorder);
            _iterCount = 0;

            while (true)
            {
                _iterCount++;
                Console.WriteLine($"Iteration: {_iterCount}, current interval: [{left};{right}], " +
                                  $"x min:{(right + left) / 2}, Amount of function calls: {_function.AmountFunctionCalls}");
                
                // Если дошли до нужной точности, возвращается середина между границами.
                if (!(interval > exactitude))
                {
                    var finalPoint = (left + right) / 2;
                    return new PointAndValue(finalPoint, _function.CalculateFunction(finalPoint));
                }
                
                // Если функция от левой границы окрестности больше, отсекаем часть от исходной левой границы до окрестности.
                if (functionLeftBorder < functionRightBorder)
                {
                    right = rightBorder;
                    rightBorder = leftBorder;
                    interval = Math.Abs(right - left);
                    functionRightBorder = functionLeftBorder;

                    leftBorder = left + interval / (_proportion + 1);
                    functionLeftBorder = _function.CalculateFunction(rightBorder);
                    continue;
                }

                // Если функция от правой границы окрестности больше, отсекаем часть от окрестности до исходной правой границы.
                left = leftBorder;
                leftBorder = rightBorder;
                functionLeftBorder = functionRightBorder;
                interval = Math.Abs(right - left);

                rightBorder = right - interval / (_proportion + 1);
                functionRightBorder = _function.CalculateFunction(rightBorder);
            }
        }
    }
}