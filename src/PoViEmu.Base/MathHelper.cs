using System;

namespace PoViEmu.Base
{
    public static class MathHelper
    {
        public static bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }
        
        public static int HighestPowerOf2Below(int number)
        {
            var power = 1;
            while (power <= number / 2)
                power *= 2;
            return power;
        }
        
        public static int CorrectToNearestPowerOf2(int number)
        {
            if (IsPowerOfTwo(number))
                return number;

            var lowerPowerOf2 = HighestPowerOf2Below(number);
            var upperPowerOf2 = lowerPowerOf2 * 2;

            if (Math.Abs(number - lowerPowerOf2) < Math.Abs(number - upperPowerOf2))
            {
                return lowerPowerOf2;
            }
            return upperPowerOf2;
        }
    }
}