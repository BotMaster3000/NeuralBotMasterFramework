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
        [TestMethod]
        public void ConstructorTest_SetupLayersCorrectly()
        {
            const int INPUT_NODES = 10;
            const int HIDDEN_LAYERS = 5;
            const int HIDDEN_NODES_PER_LAYER = 10;
            const int OUTPUT_NODES = 3;
            WeightedNetwork network = new WeightedNetwork(INPUT_NODES, HIDDEN_LAYERS, HIDDEN_NODES_PER_LAYER, OUTPUT_NODES);
            Assert.AreEqual(INPUT_NODES, network.InputLayer.Nodes.Length);
            Assert.AreEqual(HIDDEN_LAYERS, network.HiddenLayers.Length);
            foreach (ILayer node in network.HiddenLayers)
            {
                Assert.AreEqual(HIDDEN_NODES_PER_LAYER, node.Nodes.Length);
            }
            Assert.AreEqual(OUTPUT_NODES, network.OutputLayer.Nodes.Length);
        }

        [TestMethod]
        public void SetInputTest()
        {
            //WeightedNetwork network = new WeightedNetwork();
            Assert.Fail();
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