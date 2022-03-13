using System;

namespace Optimization
{
    public class Function
    {
        public int AmountFunctionCalls { get; private set; }

        public Function()
        {
            AmountFunctionCalls = default;
        }

        public double CalculateFunction(double x)
        { // Your function.
            AmountFunctionCalls++;
            return Math.Exp(Math.Sin(DegreesToRadians(x))) * Math.Pow(x, 2);
        }
        
        private static double DegreesToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}