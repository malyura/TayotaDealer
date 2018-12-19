using System;

namespace Framework.Utils
{
    public class RandomUtils
    {
        private static readonly Random _random = new Random();

        public static int GetRandomNumber(int rightBound) => _random.Next(rightBound);

    }
}
