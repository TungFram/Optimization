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
        {
            AmountFunctionCalls++;
            // Ваша функция. Помните : функции Sin и т.п. по умолчанию работают с радианами, используйте DegreesToRadians.
            return Math.Sin(DegreesToRadians(x)) - Math.Log(Math.Pow(x, 2)) - 1;
        }
        
        private static double DegreesToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

// Вариант 1: Math.Sin(DegreesToRadians(x)) * Math.Pow(x, 3);
// Вариант 2: 
// Вариант 3: Math.Sin(DegreesToRadians(x)) - Math.Log(Math.Pow(x, 2)) - 1;
// Вариант 4: Math.Log(Math.Pow(x, 2)) + 1 - Math.Sin(DegreesToRadians(x));
// Вариант 5: Math.Exp(Math.Sin(DegreesToRadians(x))) * Math.Pow(x, 2);
// Вариант 6: Math.Sin(DegreesToRadians(x)) * Math.Pow(x, 2);
// Вариант 7: Math.Exp(Math.Pow(x, 2) * -1) * Math.Pow(x, 2) + (1 - Math.E - Math.Pow(x, 2)) * Math.Sin(DegreesToRadians(x));
// Вариант 8: Math.Log(x) * Math.Sin(DegreesToRadians(x)) * Math.Pow(x, 2);
// Вариант 9: Math.Exp(Math.Sin(DegreesToRadians(x)) * Math.Log(x));
// Вариант 10: ?
