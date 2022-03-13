using System;

namespace Optimization
{
    public class CombinedBrentMethodWiki_Fail
    {
        // Алгоритм взят отсюда:
        // https://en.wikipedia.org/wiki/Brent%27s_method
        private readonly Function _function;
        public CombinedBrentMethodWiki_Fail()
        {
            _function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            double functionLeftBorder = _function.CalculateFunction(left);
            double functionRightBorder = _function.CalculateFunction(right);

            if (functionLeftBorder * functionRightBorder >= 0) return null; // Между этими границами нет корня?
            if (Math.Abs(functionLeftBorder) < Math.Abs(functionRightBorder))
                (right, left) = (left, right);

            double help = left;
            // double prevHelp = 0;
            double root = 0;
            // bool flag = true;

            while (true)
            {
                var functionHelp = _function.CalculateFunction(help);
                if (Math.Abs(_function.CalculateFunction(help) - _function.CalculateFunction(left)) > 0.000001 ||
                    Math.Abs(_function.CalculateFunction(help) - _function.CalculateFunction(right)) > 0.000001)
                {
                    root = left * functionRightBorder * functionHelp /
                           ((functionLeftBorder - functionRightBorder) * (functionLeftBorder - functionHelp))
                           +
                           right * functionLeftBorder * functionHelp /
                           ((functionRightBorder - functionLeftBorder) * (functionRightBorder - functionHelp))
                           +
                           help * functionLeftBorder * functionRightBorder /
                           ((functionHelp - functionLeftBorder) * (functionHelp - functionRightBorder));
                }
                else
                {
                    root = right - functionRightBorder * (right - left) / (functionRightBorder - functionLeftBorder);
                }

                return null;
                /*if (root < ((3 * left + right) / 4) || root > right ||
                    (flag && Math.Abs(root - right) >= Math.Abs(right - help) / 2) ||
                    (!flag && Math.Abs(root - right) >= Math.Abs(help - prevHelp) / 2) ||
                    (flag && Какая в жопу δ?! Откуда эта δ вообще взялась?)*/
            }
        }

    }
}