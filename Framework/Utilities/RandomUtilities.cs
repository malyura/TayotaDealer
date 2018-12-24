using System;

namespace Framework.Utilities
{
    public class RandomUtilities
    {
        private static readonly Random _random = new Random();

        public static int GetRandomNumber(int rightBound) => _random.Next(rightBound);

    }
}
