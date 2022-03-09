namespace Optimization
{
    public class PointAndValue
    {
        public PointAndValue(double point, double value)
        {
            Point = point;
            Value = value;
        }
        public double Point { get; }
        public double Value { get; }
    }
}