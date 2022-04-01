using System;
using System.IO;

namespace Optimization
{
    class Program
    {
        public static FileStream fileOutput = File.Open("output.csv", FileMode.Create);
        public static StreamWriter swOutput = new StreamWriter(fileOutput);
        static void Main(string[] args)
        {
            swOutput.WriteLine("Iteration;Interval;x min;Amount of function calls");
           //init vals
            double epsilon = 0.000001;
            double left  = -10;
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
            
            //Parabola method
            ParabolaMethod parabolaMethod = new ParabolaMethod();
            var parabolaMin = parabolaMethod.Min(left, right, epsilon);
            
            //Brent method
            var brentCombinedMethod1 = new BrentCombinedMethod1();
            var brent1Min = brentCombinedMethod1.Min(left, right, epsilon);
            
            var brentCombinedMethod2 = new BrentCombinedMethod2();
            var brent2Min = brentCombinedMethod2.Min(left, right, epsilon);
            
            var brentCombinedMethod3 = new BrentCombinedMethod3();
            var brent3Min = brentCombinedMethod3.Min(left, right, epsilon);

            Space();
            Console.WriteLine("\n\nDichotomy method:");
            Console.WriteLine($"Minimum: ( {dichotomyMin.Point} ; {dichotomyMin.Value} ), amount of iterations: {dichotomyMethod.IterationCount}," +
                              $" amount of function calls: {dichotomyMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Golden Ratio Method:");
            Console.WriteLine($"Minimum: ( {goldenMin.Point} ; {goldenMin.Value} ), amount of iterations: {goldenRatioMethod.IterationCount}," +
                              $" amount of function calls: {goldenRatioMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Fibonacci method:");
            Console.WriteLine($"Minimum: ( {fibonacciMin.Point} ; {fibonacciMin.Value} ), amount of iterations: {fibonacciMethod.IterationCount}," +
                              $" amount of function calls: {fibonacciMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Parabola method:");
            Console.WriteLine($"Minimum: ( {parabolaMin.Point} ; {parabolaMin.Value} ), amount of iterations: {parabolaMethod.IterationCount}," +
                              $" amount of function calls: {parabolaMethod.Function.AmountFunctionCalls}");
            Space();
            
            Console.WriteLine("Brent Method, way 1:");
            Console.WriteLine($"Minimum: ( {brent1Min.Point} ; {brent1Min.Value} ), amount of iterations: {brentCombinedMethod1.IterationCount}," +
                              $" amount of function calls: {brentCombinedMethod1.Function.AmountFunctionCalls}");
            Console.WriteLine("Brent Method, way 2:");
            Console.WriteLine($"Minimum: ( {brent2Min.Point} ; {brent2Min.Value} ), amount of iterations: {brentCombinedMethod2.IterationCount}," +
                              $" amount of function calls: {brentCombinedMethod2.Function.AmountFunctionCalls}");
            Console.WriteLine("Brent Method, way 3, main:");
            Console.WriteLine($"Minimum: ( {brent3Min.Point} ; {brent3Min.Value} ), amount of iterations: {brentCombinedMethod3.IterationCount}," +
                              $" amount of function calls: {brentCombinedMethod3.Function.AmountFunctionCalls}");
            swOutput.Close();
            fileOutput.Close();
        }

        private static void Space()
        {
            Console.WriteLine("-------------------------------------------------");
        }
    }
}