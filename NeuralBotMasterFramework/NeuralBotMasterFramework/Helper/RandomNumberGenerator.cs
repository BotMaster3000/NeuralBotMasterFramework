using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Helper
{
    public static class RandomNumberGenerator
    {
        private static readonly Random rand = new Random();

        public static double GetNextDouble()
        {
            return rand.NextDouble();
        }

        public static int GetNextNumber(int numberFrom, int numberTo)
        {
            return rand.Next(numberFrom, numberTo + 1);
        }
    }
}
