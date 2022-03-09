using System;
using System.Collections.Generic;

namespace Optimization
{
    public class FibonacciFunction
    {
    public double calc(double a, double b, double eps)
    {
        amountOfFuncComputation = 0;
        double previousLenght = b - a;
        List<int> fibonacciSeries = new List<int>{1, 1};
        while(true)
        {
            int newItem = fibonacciSeries[^1] + fibonacciSeries[^2];
            fibonacciSeries.Add(newItem);
            if(newItem > Math.Abs(b - a) / eps)
                break;
        }
        int n = fibonacciSeries.Count - 1;
        double x1 = a + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (b - a);
        double x2 = a + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (b - a);
        double fx1 = f(x1);
        double fx2 = f(x2);
        while(n > 2)
        {
            Console.WriteLine("Iteration: " + iterAmount + ", Current interval: (" + a + ", " + b + ")" + ", difference between previous interval: " + (previousLenght / Math.Abs(b - a)) + /*", xmin: " + ((a + b) / 2) + */ ", Current amount of func calculations: " + amountOfFuncComputation);
            n--;
            iterAmount++;
            previousLenght = b - a;
            if (fx1 < fx2)
            {
                b = x2;
                x2 = x1;
                fx2 = fx1;
                x1 = a + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (b - a);
                fx1 = f(x1);
            }
            else
            {
                a = x1;
                x1 = x2;
                fx1 = fx2;
                x2 = a + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (b - a);
                fx2 = f(x2);
            }
                
        }
        return ((a + b) / 2);
    }
    }
}