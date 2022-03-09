using System;
using System.Collections.Generic;

namespace Optimization
{
    public class FibonacciFunction
    {
        private readonly Function _function;
        private int _iterCount;
        public FibonacciFunction()
        {
            _function = new Function();
        }
        public double Min(double left, double right, double exactitude = 0.001)
        {
            double previousLenght = right - left;
            List<int> fibonacciSeries = new List<int>{1, 1};
            while(true)
            {
                int newItem = fibonacciSeries[^1] + fibonacciSeries[^2];
                fibonacciSeries.Add(newItem);
                i_function.CalculateFunction(newItem > Math.Abs(right - left) / exactitude)
                    break;
            }
            int n = fibonacciSeries.Count - 1;
            double leftBorder = left + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (right - left);
            double rightBorder = left + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (right - left);
            double functionLeftBorder = _function.CalculateFunction(leftBorder);
            double functionRightBorder = _function.CalculateFunction(rightBorder);
            while(n > 2)
            {
                Console.WriteLine("Iteration: " + _iterCount + ", Current interval: (" + left + ", " + right + ")" + ", difference between previous interval: " + (previousLenght / Math.Abs(right - left)) + /*", xmin: " + ((a + b) / 2) + */ ", Current amount of func calculations: " + amountOfFuncComputation);
                n--;
                iterCount++;
                previousLenght = right - left;
                if (functionLeftBorder < functionRightBorder)
                {
                    right = rightBorder;
                    rightBorder = leftBorder;
                    functionRightBorder = functionLeftBorder;
                    leftBorder = left + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (right - left);
                    functionLeftBorder = _function.CalculateFunction(leftBorder);
                }
                else
                {
                    left = leftBorder;
                    leftBorder = rightBorder;
                    functionLeftBorder = functionRightBorder;
                    rightBorder = left + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (right - left);
                    functionRightBorder = _function.CalculateFunction(rightBorder);
                }
                    
            }
            return ((left + right) / 2);
        }
    }
}