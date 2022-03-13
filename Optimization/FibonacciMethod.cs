﻿using System;
using System.Collections.Generic;

namespace Optimization
{ // Не знаю, я читал про этот метод, и мысль примерно ясна, но с реализацией я так и не разобрался, ибо толлком нигде не объясняют.
    public class FibonacciMethod
    {
        public readonly Function Function;
        public int IterationCount;
        private readonly List<int>_fibonacciSequence = new List<int>{1, 1};
        public FibonacciMethod()
        {
            Function = new Function();
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
            double functionLeftBorder  = Function.CalculateFunction(leftBorder);
            double functionRightBorder = Function.CalculateFunction(rightBorder);
            
            while(n > 2)
            {
                Console.WriteLine($"[Fibonacci] Iteration: {IterationCount}, current interval: [{left},{right}]," +
                                  $" x min:{(right + left) / 2}, amount of function calls: {Function.AmountFunctionCalls}");
                n--;
                IterationCount++;
                previousLenght = right - left;
                if (functionLeftBorder < functionRightBorder) // Если слева меньше, сдвигаемся влево.
                {
                    right = rightBorder;
                    rightBorder = leftBorder;
                    functionRightBorder = functionLeftBorder;
                    leftBorder = left + ((double)_fibonacciSequence[n - 2] / _fibonacciSequence[n]) * previousLenght;
                    functionLeftBorder = Function.CalculateFunction(leftBorder);
                }
                else // Если справа меньше, соответственно, сдвигаем границы вправо.
                {
                    left = leftBorder;
                    leftBorder = rightBorder;
                    functionLeftBorder = functionRightBorder;
                    rightBorder = left + ((double)_fibonacciSequence[n - 1] / _fibonacciSequence[n]) * previousLenght;
                    functionRightBorder = Function.CalculateFunction(rightBorder);
                }
                    
            }
            
            var finalPoint = (left + right) / 2;
            return new PointAndValue(finalPoint, Function.CalculateFunction(finalPoint));
        }
    }
}