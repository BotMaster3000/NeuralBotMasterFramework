using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Models.Tests
{
    [TestClass]
    public class WeightedLayerTests
    {
        [TestMethod]
        public void AssignPropertyTest()
        {
            Random rand = new Random();
            IWeightedNode[] weightedNodes = new IWeightedNode[]
            {
                new WeightedNode(){ Weights = new double[]{ rand.NextDouble(), rand.NextDouble() } },
                new WeightedNode(){ Weights = new double[]{ rand.NextDouble(), rand.NextDouble() } }
            };
            WeightedLayer layer = new WeightedLayer()
            {
                Nodes = weightedNodes,
            };
            Assert.AreEqual(weightedNodes, layer.Nodes);
        }

        [TestMethod]
        public void ConstructorTest_SetupNodesCorrectly()
        {
            const int TOTAL_NODES = 15;
            WeightedLayer layer = new WeightedLayer(TOTAL_NODES);
            Assert.AreEqual(TOTAL_NODES, layer.Nodes.Length);
        }
    }
}