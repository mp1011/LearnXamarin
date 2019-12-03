using System;

namespace LearnXamarin.Services
{

    public class RandomService
    {
        private readonly Random _rng;

        public RandomService()
        {
            _rng = new Random();
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
