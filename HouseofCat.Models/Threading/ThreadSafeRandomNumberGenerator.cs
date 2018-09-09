using System;

namespace HouseofCat.Models
{
    public class ThreadSafeRandomNumberGenerator
    {
        //Notes: https://blogs.msdn.microsoft.com/pfxteam/2009/02/19/getting-random-numbers-in-a-thread-safe-way/

        private Random _global = new Random();
        [ThreadStatic]
        private Random _local;

        public int Next()
        {
            var inst = _local;

            if (inst == null)
            {
                var seed = 0;

                lock (_global)
                { seed = _global.Next(); }

                _local = inst = new Random(seed);
            }

            return inst.Next();
        }

        public int Next(int minValue, int maxValue)
        {
            var inst = _local;

            if (inst == null)
            {
                var seed = 0;

                lock (_global)
                { seed = _global.Next(); }

                _local = inst = new Random(seed);
            }

            return inst.Next(minValue, maxValue);
        }
    }
}
