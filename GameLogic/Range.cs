using System;

namespace cs_prog_17
{
    public class Range<T> where T : IComparable<T>
    {
        public T Min { get; private set; }
        public T Max { get; private set; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
            {
                Min = max;
                Max = min;
            }
            else
            {
                Min = min;
                Max = max;
            }
        }
    }
}
