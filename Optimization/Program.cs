using System;

namespace Optimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //init vals
            double epsilon = 0.1;
            double left = -10;
            double right = 10;

            //Dichotomy method
             var dichotomyMethod = new DichotomyMethod();
             var dichotomyMin = dichotomyMethod.Min(left, right, epsilon);
             
             
            //Golden method
            var goldenRatioMethod = new GoldenRatioMethod();
            var goldenMin = goldenRatioMethod.Min(left, right, epsilon);
            
            //Fibonacci method
            var fibonacciMethod = new FibonacciMethod();
            var fibonacciMin = fibonacciMethod.Min(left, right, epsilon);
            
            /*
            //Parabola method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Parabola method: ");
            ParabolaMethod pmethod = new ParabolaMethod();
            xmin = pmethod.calc(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin));*/
            
            //Brent method
            var brentCombinedMethod = new BrentCombinedMethod();
            var brentMin = brentCombinedMethod.Min(left, right, epsilon);
            
            Console.WriteLine("Dichotomy method:");
            Console.WriteLine($"Minimum: ({dichotomyMin.Point};{dichotomyMin.Value}), amount of iterations: {dichotomyMethod.IterationCount}," +
                              $" amount of function calls: {dichotomyMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Golden Ratio Method:");
            Console.WriteLine($"Minimum: ({goldenMin.Point};{goldenMin.Value}), amount of iterations: {goldenRatioMethod.IterationCount}," +
                              $" amount of function calls: {goldenRatioMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Fibonacci method:");
            Console.WriteLine($"Minimum: ({fibonacciMin.Point};{fibonacciMin.Value}), amount of iterations: {fibonacciMethod.IterationCount}," +
                              $" amount of function calls: {fibonacciMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Brent Method:");
            Console.WriteLine($"Minimum: ({brentMin.Point};{brentMin.Value}), amount of iterations: {brentCombinedMethod.IterationCount}," +
                              $" amount of function calls: {brentCombinedMethod.Function.AmountFunctionCalls}");
        }

        private static void Space()
        {
            Console.WriteLine("-------------------------------------------------");
        }
    }
}