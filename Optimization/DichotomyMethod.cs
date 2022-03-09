using System;

namespace Optimization
{
    public class DichotomyMethod
    {
        private readonly Function _function;
        public DichotomyMethod()
        {
            _function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            int iterAmount = 0;
            while (true)
            {
                iterAmount++;
                double leftBorder = (left + right) / 2 - exactitude;
                double rightBorder = (left + right) / 2 + exactitude;

                double functionLeftBorder = _function.CalculateFunction(leftBorder);
                double functionRightBorder = _function.CalculateFunction(rightBorder);

                Console.WriteLine($"Iteration: {iterAmount}, current interval: [{left},{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {_function.AmountFunctionCalls}");

                // Если дошли до нужной точности, возвращается середина между границами.
                if (!(Math.Abs(right - left) > exactitude))
                {
                    var finalPoint = (left + right) / 2;
                    return new PointAndValue(finalPoint, _function.CalculateFunction(finalPoint));
                }

                // Если функция от левой границы окрестности больше, отсекаем часть от исходной левой границы до окрестности.
                if (functionLeftBorder > functionRightBorder) 
                {
                    left = leftBorder;
                    continue;
                }

                // Если функция от правой границы окрестности больше, отсекаем часть от окрестности до исходной правой границы.
                right = rightBorder;
            }
        }
    }
}