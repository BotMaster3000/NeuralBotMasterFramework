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
    public class NodeTests
    {
        [TestMethod]
        public void NodeTest_PropertyTest()
        {
            Random rand = new Random();
            double value = rand.NextDouble();
            Node node = new Node
            {
                Value = value
            };
            Assert.AreEqual(value, node.Value);
        }
    }
}