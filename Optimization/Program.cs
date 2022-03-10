using System;

namespace Optimization
{
    class Program
    {
        static void Main(string[] args)
        {
            //init vals
            double epsilon = 0.001;
            double left = 0;
            double right = 6;
            double xmin = 0;
            
             //dichotomy method
             Console.WriteLine("Dichotomy method:");
             DichotomyMethod dichotomyMethod = new DichotomyMethod();
             xmin = dichotomyMethod.Min(epsilon, left, right);
             Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + dichotomyMethod.getIterAmount() + ", amount of func calculations: " + dichotomyMethod.getAmountOfComputations());
             
            //Golden method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Golden Ratio Method: ");
            GoldenRatioMethod goldenRatioMethod = new GoldenRatioMethod();
            xmin = goldenRatioMethod.Min(epsilon, left, right);
            Console.WriteLine(xmin + ", yval: " + f(xmin));
            
            //Fibonnachi method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Fibbonachi method: ");
            FibonacciMethod fibonacciMethod = new FibonacciMethod();
            xmin = fibonacciMethod.Min(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + fibonacciMethod.getIterAmount() + ", amount of func calculations: " + fibonacciMethod.getAmountOfComputations());
            
            /*
            //Parabola method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Parabola method: ");
            ParabolaMethod pmethod = new ParabolaMethod();
            xmin = pmethod.calc(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin));*/
            
            //Brent method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Brent Method: ");
            BrentCombinedMethod_Fail bmethod2 = new BrentCombinedMethod_Fail();
            xmin = bmethod2.Min(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + bmethod2.getIterAmount() + ", amount of func calculations: " + bmethod2.getAmountOfComputations());
        }
        static double f(double x)
        {
            return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
        }
    }
}