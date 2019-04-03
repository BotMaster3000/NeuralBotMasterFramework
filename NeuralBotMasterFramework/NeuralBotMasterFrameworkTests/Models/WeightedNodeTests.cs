using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Models.Tests
{
    [TestClass]
    public class WeightedNodeTests
    {
        [TestMethod]
        public void AssignPropertyTest()
        {
            Random rand = new Random();
            double value = rand.NextDouble();
            double[] weights = new double[]
            {
                rand.NextDouble(),
                rand.NextDouble(),
                rand.NextDouble(),
            };
            WeightedNode node = new WeightedNode()
            {
                Value = value,
                Weights = weights
            };
            Assert.AreEqual(value, node.Value);
            Assert.AreEqual(weights, node.Weights);
        }

        [TestMethod]
        public void ApplyWeightTest()
        {
            Assert.Fail();
        }
    }
}