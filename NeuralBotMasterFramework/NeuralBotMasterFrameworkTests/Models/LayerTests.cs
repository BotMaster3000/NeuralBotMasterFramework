using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Interfaces;

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
    }
}