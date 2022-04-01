using System;

namespace Optimization
{
    public class BrentCombinedMethod3
    {
        public readonly Function Function;
        public int IterationCount;
        private readonly double _proportion = (3 - Math.Sqrt(5)) / 2;

        public BrentCombinedMethod3()
        {
            Function = new Function();
        }

        public PointAndValue Min(double left, double right, double exactitude = 0.001)
        {
            IterationCount = 0;
            var min = left + _proportion * (right - left);
            var secondFromBelow = min;
            var prevSecondFromBelow = secondFromBelow;
            var e = 0.0;
            var functionMin = Function.CalculateFunction(min);
            var functionSecondFromBelow = functionMin;
            var functionPrevSecondFromBelow = functionSecondFromBelow;
            while (true)
            {
                System.Console.WriteLine($"[Brent3] Iteration: {IterationCount}, current interval: [{left};{right}], " +
                                         $"x min:{(right + left) / 2}, Amount of function calls: {Function.AmountFunctionCalls}");
                var middle = (left + right) / 2;
                var tol = exactitude * Math.Abs(min) + exactitude;
                //
                // Check the stopping criterion.
                //
                if (Math.Abs(min - middle) <= tol * 2 - (right - left) / 2)
                {
                    break;
                }

                //
                // Fit a parabola.
                //
                var r = 0.0;
                var q = r;
                var p = q;
                double d = 0;
                if (tol < Math.Abs(e))
                {
                    r = (min - secondFromBelow) * (functionMin - functionPrevSecondFromBelow);
                    q = (min - prevSecondFromBelow) * (functionMin - functionSecondFromBelow);
                    p = (min - prevSecondFromBelow) * q - (min - secondFromBelow) * r;
                    q = 2.0 * (q - r);
                    if (0.0 < q)
                    {
                        p = -p;
                    }

                    q = Math.Abs(q);
                    r = e;
                    e = d;
                }

                double minOfParabola;
                if (Math.Abs(p) < Math.Abs(0.5 * q * r) &&
                    q * (left - min) < p &&
                    p < q * (right - min))
                {
                    //
                    // Take the parabolic interpolation step.
                    //
                    d = p / q;
                    minOfParabola = min + d;
                    //
                    // F must not be evaluated too close to A or B.
                    //
                    if ((minOfParabola - left) < 2 * tol || (right - minOfParabola) < 2 * tol)
                    {
                        if (min < middle)
                        {
                            d = tol;
                        }
                        else
                        {
                            d = -tol;
                        }
                    }
                }
                //
                // A golden-section step.
                //
                else
                {
                    if (min < middle)
                    {
                        e = right - min;
                    }
                    else
                    {
                        e = left - min;
                    }

                    d = _proportion * e;
                }

                //
                // F must not be evaluated too close to X.
                //
                if (tol <= Math.Abs(d))
                {
                    minOfParabola = min + d;
                }
                else if (0.0 < d)
                {
                    minOfParabola = min + tol;
                }
                else
                {
                    minOfParabola = min - tol;
                }

                var functionMinOfParabola = Function.CalculateFunction(minOfParabola);
                //
                // Update A, B, V, W, and X.
                //
                if (functionMinOfParabola <= functionMin)
                {
                    if (minOfParabola < min)
                    {
                        right = min;
                    }
                    else
                    {
                        left = min;
                    }

                    prevSecondFromBelow = secondFromBelow;
                    functionPrevSecondFromBelow = functionSecondFromBelow;
                    secondFromBelow = min;
                    functionSecondFromBelow = functionMin;
                    min = minOfParabola;
                    functionMin = functionMinOfParabola;
                }
                else
                {
                    if (minOfParabola < min)
                    {
                        left = minOfParabola;
                    }
                    else
                    {
                        right = minOfParabola;
                    }

                    if (functionMinOfParabola <= functionSecondFromBelow || secondFromBelow == min)
                    {
                        prevSecondFromBelow = secondFromBelow;
                        functionPrevSecondFromBelow = functionSecondFromBelow;
                        secondFromBelow = minOfParabola;
                        functionSecondFromBelow = functionMinOfParabola;
                    }
                    else if (functionMinOfParabola <= functionPrevSecondFromBelow || prevSecondFromBelow == min || prevSecondFromBelow == secondFromBelow)
                    {
                        prevSecondFromBelow = minOfParabola;
                        functionPrevSecondFromBelow = functionMinOfParabola;
                    }
                }

                IterationCount++;
            }

            var finalPoint = functionMin;
            return new PointAndValue(finalPoint, Function.CalculateFunction(finalPoint));
        }
    }
}