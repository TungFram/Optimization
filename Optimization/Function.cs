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
            return Math.Exp(Math.Sin(x)) * Math.Pow(x, 2);
        }
    }
}