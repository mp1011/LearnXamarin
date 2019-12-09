using System;

namespace LearnXamarin.Services
{

    public class RandomService
    {
        private Random _rng;

        public RandomService()
        {
#if DEBUG
            _rng = new Random(1000);
#else
            _rng = new Random();
#endif
        }

        public void SetRandomSeed(int seed)
        {
            _rng = new Random(seed);
        }

        public int RandomNumber(int minInclusive, int maxExclusive)
        {
            return minInclusive + _rng.Next(maxExclusive - minInclusive);
        }

        public T RandomElement<T>(T[] array)
        {
            var index = RandomNumber(0, array.Length);
            return array[index];
        }
    }
}
