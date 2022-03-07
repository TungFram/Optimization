using System;

namespace Optimization
{
    public abstract class Method
    {
        public static int interamount;
        public static int amountoffunccomputation;
        public static double f(double x)
        {
            amountoffunccomputation++;
            return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
        }
        public int getAmountOfComputations()
        {
            return amountoffunccomputation;
        }

        public int getIterAmount()
        {
            return interamount;
        }
    }
}