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
        public void SetValueTest()
        {
            Random rand = new Random();
            double[] weights = new double[]
            {
                rand.NextDouble(),
                rand.NextDouble(),
            };
            double[] values = new double[]
            {
                rand.NextDouble(),
                rand.NextDouble(),
            };
            WeightedNode node = new WeightedNode()
            {
                Weights = weights,
            };
            node.SetValue(values);
            double weightedValue = CalculateWeights(values, weights);
            Assert.AreEqual(weightedValue, node.Value);
        }

        private double CalculateWeights(double[] values, double[] weights)
        {
            Assert.AreEqual(values.Length, weights.Length);
            double value = 0;
            for (int i = 0; i < values.Length; ++i)
            {
                value += values[i] * weights[i];
            }
            return value;
        }

        [TestMethod]
        public void ConstructorTest_GeneratesRandomStarterWeight()
        {
            const int PREVIOUS_LAYER_NODES = 42;
            WeightedNode node = new WeightedNode(PREVIOUS_LAYER_NODES);
            Assert.AreEqual(PREVIOUS_LAYER_NODES, node.Weights.Length);

            double previousNumber = 0.0;
            foreach(double weight in node.Weights)
            {
                Assert.IsTrue(weight != 0.0);
                Assert.AreNotEqual(weight, previousNumber);
                previousNumber = weight;
            }
        }
    }
}