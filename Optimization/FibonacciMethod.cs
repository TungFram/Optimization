using System;
using System.Collections.Generic;

namespace Optimization
{ // Не знаю, я читал про этот метод, и мысль примерно ясна, но с реализацией я так и не разобрался, ибо толлком нигде не объясняют.
    public class FibonacciMethod
    {
        private readonly Function _function;
        private int _iterCount;
        private readonly List<int>_fibonacciSequence = new List<int>{1, 1};
        public FibonacciMethod()
        {
            _function = new Function();
        }
        
        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            double previousLenght = right - left;
            while(true) // Cтроим последовательность Фибоначчи нужной длины
            {
                int newElement = _fibonacciSequence[^2] + _fibonacciSequence[^1];
                _fibonacciSequence.Add(newElement);
                if(newElement > Math.Abs(previousLenght) / exactitude)
                    break;
            }
            
            int n = _fibonacciSequence.Count - 1;
            double leftBorder  = left + ((double)_fibonacciSequence[n - 2] / _fibonacciSequence[n] * previousLenght);
            double rightBorder = left + ((double)_fibonacciSequence[n - 1] / _fibonacciSequence[n] * previousLenght);
            double functionLeftBorder  = _function.CalculateFunction(leftBorder);
            double functionRightBorder = _function.CalculateFunction(rightBorder);
            
            while(n > 2)
            {
                Console.WriteLine($"Iteration: {_iterCount}, current interval: [{left},{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {_function.AmountFunctionCalls}");
                n--;
                _iterCount++;
                previousLenght = right - left;
                if (functionLeftBorder < functionRightBorder) // Если слева меньше, сдвигаемся влево.
                {
                    right = rightBorder;
                    rightBorder = leftBorder;
                    functionRightBorder = functionLeftBorder;
                    leftBorder = left + ((double)_fibonacciSequence[n - 2] / _fibonacciSequence[n]) * previousLenght;
                    functionLeftBorder = _function.CalculateFunction(leftBorder);
                }
                else // Если справа меньше, соответственно, сдвигаем границы вправо.
                {
                    left = leftBorder;
                    leftBorder = rightBorder;
                    functionLeftBorder = functionRightBorder;
                    rightBorder = left + ((double)_fibonacciSequence[n - 1] / _fibonacciSequence[n]) * previousLenght;
                    functionRightBorder = _function.CalculateFunction(rightBorder);
                }
                    
            }
            
            var finalPoint = (left + right) / 2;
            return new PointAndValue(finalPoint, _function.CalculateFunction(finalPoint));
        }
    }
}