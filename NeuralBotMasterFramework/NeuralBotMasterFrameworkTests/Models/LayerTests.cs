using NeuralBotMasterFramework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Helper;

namespace NeuralBotMasterFramework.Models.Tests
{
    [TestClass]
    public class LayerTests
    {
        [TestMethod]
        public void AssignPropertyTest()
        {
            INode[] nodes = new INode[]
            {
                new Node(){Value = 0.152},
                new Node(){ Value = 0.1512},
            };
            Layer layer = new Layer
            {
                Nodes = nodes
            };
            Assert.AreEqual(nodes, layer.Nodes);
        }

        [TestMethod]
        public void ConstructorTest_SetupNodesCorrectly()
        {
            const int TOTAL_NODES = 10;
            Layer layer = new Layer(TOTAL_NODES);
            Assert.AreEqual(TOTAL_NODES, layer.Nodes.Length);
        }

        [TestMethod]
        public void Set_And_Get_ValuesTest()
        {
            const int TOTAL_NODES = 3;

            double[] input = new double[TOTAL_NODES]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            };

            Layer layer = new Layer(TOTAL_NODES);
            layer.SetValues(input);
            double[] results = layer.GetValues();
            for(int i = 0; i < input.Length; ++i)
            {
                Assert.AreEqual(input[i], results[i]);
            }
        }
    }
}