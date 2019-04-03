using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Helper.Tests
{
    [TestClass]
    public class RandomNumberGeneratorTests
    {
        [TestMethod]
        public void GetNextDoubleTest()
        {
            const int TOTAL_ITERATIONS = 1000;
            double previousValue = 0.0;
            for(int i = 0; i < TOTAL_ITERATIONS; ++i)
            {
                double currentValue = RandomNumberGenerator.GetNextDouble();
                Assert.IsTrue(currentValue >= 0);
                Assert.AreNotEqual(previousValue, currentValue);
                previousValue = currentValue;
            }
        }
    }
}