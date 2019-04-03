using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Models;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Logic.Tests
{
    [TestClass]
    public class WeightedNetworkTests
    {
        private const int INPUT_NODES = 3;
        private const int HIDDEN_LAYERS = 5;
        private const int HIDDEN_NODES_PER_LAYER = 10;
        private const int OUTPUT_NODES = 3;
        private readonly WeightedNetwork network = new WeightedNetwork(INPUT_NODES, HIDDEN_LAYERS, HIDDEN_NODES_PER_LAYER, OUTPUT_NODES);

        [TestMethod]
        public void ConstructorTest_SetupLayersCorrectly()
        {
            Assert.AreEqual(INPUT_NODES, network.InputLayer.Nodes.Length);
            Assert.AreEqual(HIDDEN_LAYERS, network.HiddenLayers.Length);
            foreach (IWeightedLayer node in network.HiddenLayers)
            {
                Assert.AreEqual(HIDDEN_NODES_PER_LAYER, node.Nodes.Length);
            }
            Assert.AreEqual(OUTPUT_NODES, network.OutputLayer.Nodes.Length);
        }

        [TestMethod]
        public void SetInputTest()
        {
            Random rand = new Random();
            double[] input = new double[INPUT_NODES]
            {
                rand.NextDouble(),
                rand.NextDouble(),
                rand.NextDouble(),
            };
            network.SetInput(input);
        }

        [TestMethod]
        public void GetOutputTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PropagateTest()
        {
            Assert.Fail();
        }
    }
}