using System;

namespace Optimization
{
    public class DichotomyMethod //TODO: Почему на одну итерацию больше чем нужно
    {
        private double xmin;
        private readonly Function _function;
        public DichotomyMethod()
        {
            _function = new Function();
        }

        public double Min(double left, double right, double exactitude)
        {
            while (true)
            {
                xmin = 0;
                double leftSide = (left + right) / 2 - exactitude;
                double rightSide = (left + right) / 2 + exactitude;

                double functionLeftSide = _function.CalculateFunction(leftSide);
                double functionRightSide = _function.CalculateFunction(rightSide);

                if (Math.Abs(right - left) > exactitude)
                {
                    if (functionLeftSide > functionRightSide)
                    {
                        left = leftSide;
                        continue;
                    }

                    if (functionLeftSide < functionRightSide)
                    {
                        right = rightSide;
                        continue;
                    }

                    left = leftSide;
                    right = rightSide;
                    Console.WriteLine("Interation: " + iterAmount + ", Current interval: (" + left + ", " + right + ")" + ", xmin: " + ((right + left) / 2) + ", Current amount of func calculations: " + _function.AmountFunctionCalls);
                }

                xmin = (left + right) / 2;
                return xmin;
                break;
            }
        }
    }
}